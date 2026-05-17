using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.Catalog.Services;

/// <summary>
/// A class that implements a X4 catalog file reader.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="xmlReaderWrapperFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XML reader factory to use.</param>
internal class CatalogFileReader(
                                 IDataParser dataParser,
                                 ILogger<CatalogFileReader> logger,
                                 IXmlReaderWrapperFactory xmlReaderWrapperFactory)
    : ICatalogFileReader
{
    private const string FileExtensionDataFile = $".{Constants.CatalogFile.FileExtensions.DataFile}";
    private const string FileFilterIndexFile = $"*.{Constants.CatalogFile.FileExtensions.IndexFile}";
   
    private readonly Encoding _encoding = Encoding.UTF8;

    private static string GetDataPath(string catalogPath, int catalogId)
        => $"{GetPath(catalogPath, catalogId)}.{Constants.CatalogFile.FileExtensions.DataFile}";

    private async Task<ICatalogIndexEntry> GetFirstIndexEntry(string indexPath, string? fileFilter, string? fileExtension)
    {
        var indexFiles = await ParseIndexInternal(indexPath, fileFilter, fileExtension);
        if (!indexFiles.Entries.Any())
        {
            logger.LogWarning("The index file '{IndexFile}' contained no entries", indexPath);
            throw new ArgumentException("The specified index file contained no entries", nameof(indexPath)); 
        }

        var returnValue = indexFiles.Entries.FirstOrDefault();
        if (returnValue is null)
        {
            logger.LogWarning("The index file '{IndexFile}' contained no entry matching '{FileFilter}'", indexPath, fileFilter);
            throw new ArgumentException("No entry was found in the index matching the specified filter", nameof(fileFilter));
        }

        return returnValue;
    }

    private static string GetIndexPath(string catalogPath, int catalogId)
        => $"{GetPath(catalogPath, catalogId)}.{Constants.CatalogFile.FileExtensions.IndexFile}";

    private static string GetPath(string catalogPath, int catalogId)
        => Path.Combine(catalogPath, $"{catalogId:00}");

    private string GetValidatedDataPath(string catalogPath, int catalogId)
    {
        var returnValue = GetDataPath(catalogPath, catalogId);

        if (!File.Exists(returnValue))
        {
            logger.LogWarning("The catalog data file {CatalogdataPath} does not exist", returnValue);
            throw new ArgumentException("The catalog data file does not exist", nameof(catalogId));
        }

        return returnValue;
    }

    private string GetValidatedIndexPath(string catalogPath, int catalogId)
    {
        var returnValue = GetIndexPath(catalogPath, catalogId);

        if (!File.Exists(returnValue))
        {
            logger.LogWarning("The catalog index file {CatalogindexPath} does not exist", returnValue);
            throw new ArgumentException("The catalog index file does not exist", nameof(catalogId));
        }

        return returnValue;
    }

    private async Task<ContentFileData> LoadContentFileForIndex(string indexPath)
    {
        IEnumerable<string> dependencies = [];
        string? gamePack = null;

        var directoryName = Path.GetDirectoryName(indexPath);
        if (!string.IsNullOrWhiteSpace(directoryName))
        {
            try
            {
                var contentFilePath = Path.Combine(directoryName, Constants.ContentFile.FileName.Data);
                var signatureFilePath = Path.Combine(directoryName, Constants.ContentFile.FileName.Signature);

                if (File.Exists(contentFilePath) &&
                    File.Exists(signatureFilePath))
                {
                    using var reader = xmlReaderWrapperFactory.CreateXmlReader(contentFilePath);

                    (gamePack, dependencies) = await dataParser.Parse<ContentFileData>(reader);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not load content file for index '{IndexFilePath}'", indexPath);
            }
        }

        if (string.IsNullOrWhiteSpace(gamePack))
        {
            // Check for mod
            if (Path.GetDirectoryName(indexPath)?.Contains(Constants.CatalogFile.DirectoryName.Extensions, StringComparison.OrdinalIgnoreCase) == true)
            {
                gamePack = Constants.CatalogFile.GameExtension;
            }
            else
            {
                gamePack = Constants.CatalogFile.BaseGame;
            }
        }

        return (gamePack, dependencies);
    }

    private static bool IsValidFileExtension(ICatalogIndexEntry source, string? fileExtension)
        => string.IsNullOrWhiteSpace(fileExtension) ||
           source.FilePath.EndsWith(fileExtension, StringComparison.OrdinalIgnoreCase);

    private CatalogIndexEntry ParseIndexEntry(string indexFilePath, string line, long offset)
    {
        var parts = line.Split(Constants.CatalogFile.IndexFile.ColumnSeparator, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 4 ||
            !int.TryParse(parts[^3], out var fileSize) ||
            !long.TryParse(parts[^2], out var unixTimestamp))
        {
            logger.LogWarning("The specified catalog index line '{Line}' was invalid", line);
            throw new ArgumentException("The specified catalog index line is invalid", nameof(line));
        }

        return new CatalogIndexEntry
        {
            FilePath = string.Join(Constants.CatalogFile.IndexFile.ColumnSeparator, parts[..^3]),
            Offset = offset,
            Signature = parts[^1],
            Size = fileSize,
            Timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp)
        };
    }

    private async Task<ICatalogIndex> ParseIndexInternal(string indexPath, string? fileFilter, string? fileExtension)
    {
        var entries = new List<CatalogIndexEntry>();

        var dataFilePath = Path.ChangeExtension(indexPath, FileExtensionDataFile);

        var (gamePack, dependencies) = await LoadContentFileForIndex(indexPath);

        try
        {
            var offset = 0L;

            var lines = await File.ReadAllLinesAsync(indexPath);
            foreach (var line in lines)
            {
                var entry = ParseIndexEntry(indexPath, line, offset);

                offset += entry.Size;

                if ((fileFilter is null || line.Contains(fileFilter, StringComparison.OrdinalIgnoreCase)) &&
                    IsValidFileExtension(entry, fileExtension))
                {
                    entries.Add(entry);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to parse the index file {CatalogindexPath}", indexPath);
        }

        return new CatalogIndex
        {
            DataFilePath = dataFilePath,
            Dependencies = dependencies,
            Entries = entries,
            GamePack = gamePack,
            IndexFilePath = indexPath
        };
    }

    public async Task<byte[]> ReadFromDataFile(string dataPath, long offset, int length)
    {
        await using var stream = new FileStream(
                                                dataPath,
                                                FileMode.Open,
                                                FileAccess.Read,
                                                FileShare.Read,
                                                bufferSize: 4096,
                                                useAsync: true);

        VerifyReadLength(stream.Length, offset, length, dataPath);

        stream.Seek(offset, SeekOrigin.Begin);

        byte[] returnValue = new byte[length];
        var read = await stream.ReadAsync(returnValue.AsMemory(0, length));

        VerifyReadSize(read, length, dataPath);

        return returnValue;
    }

    private async Task<string> ReadTextFileInternal(string indexPath, string dataPath, string fileFilter, string? fileExtension)
    {
        logger.LogInformation("Reading the first text file matching '{FileFilter}' from catalog {CatalogdataPath}", fileFilter, dataPath);

        var indexFile = await GetFirstIndexEntry(indexPath, fileFilter, fileExtension);

        var data = await ReadFromDataFile(dataPath, indexFile.Offset, indexFile.Size);

        return _encoding.GetString(data);
    }

    private static bool ValidateCatalogId(int catalogId)
        => catalogId > 0 && catalogId <= 99;

    private void ValidateParameters(string catalogPath)
    {
        if (!Path.Exists(catalogPath))
        {
            logger.LogWarning("The catalog path {CatalogPath} is invalid", catalogPath);
            throw new ArgumentException("The specified catalog path is invalid", nameof(catalogPath));
        }
    }

    private void ValidateParameters(string catalogPath, int catalogId)
    {
        ValidateParameters(catalogPath);

        if (!ValidateCatalogId(catalogId))
        {
            logger.LogWarning("The catalog identifier {CatalogId} is invalid", catalogId);
            throw new ArgumentException("The specified catalog identifier is invalid", nameof(catalogId));
        }
    }

    private void VerifyReadLength(long length, long offset, int size, string filePath)
    {
        if (length < offset + size)
        {
            logger.LogWarning("The specified length {Length} from {Offset} will exceed the size of {FilePath}", length, offset, filePath);
            throw new ArgumentException("The specified offset and length exceed the data file size", nameof(offset));
        }
    }

    private void VerifyReadSize(int read, int size, string filePath)
    {
        if (read < size)
        {
            logger.LogWarning("Unable to read the requested {Length} from the file {FilePath}", size, filePath);
            throw new EndOfStreamException("Could not read the requested number of bytes.");
        }
    }

    Task<IEnumerable<ICatalogIndex>> ICatalogFileReader.GetIndex(string catalogPath, bool recursiveSearch)
        => ((ICatalogFileReader)this).ParseIndexes(catalogPath, null, null, recursiveSearch);

    Task<ICatalogIndex> ICatalogFileReader.ParseIndex(string catalogPath, int catalogId, string? fileFilter, string? fileExtension)
    {
        ValidateParameters(catalogPath, catalogId);

        var indexPath = GetIndexPath(catalogPath, catalogId);

        return ParseIndexInternal(indexPath, fileFilter, fileExtension);
    }

    async Task<IEnumerable<ICatalogIndex>> ICatalogFileReader.ParseIndexes(string catalogPath, string? fileFilter, string? fileExtension, bool recursiveSearch)
    {
        var returnValue = new List<ICatalogIndex>();

        ValidateParameters(catalogPath);
        
        var searchOption = recursiveSearch
                                           ? SearchOption.AllDirectories
                                           : SearchOption.TopDirectoryOnly;

        // Get files excluding signature files
        var files = Directory.GetFiles(catalogPath, FileFilterIndexFile, searchOption);
        var indexFiles = files.Where(x => !Path.GetFileNameWithoutExtension(x).EndsWith(Constants.CatalogFile.FileName.SignatureIndicator));

        foreach (var indexFile in indexFiles)
        {
            var index = await ParseIndexInternal(indexFile, fileFilter, fileExtension);

            // Exclude mods and cataloges without any entries
            if (!index.IsGameMod &&
                index.Entries.Any())
            {
                returnValue.AddRange(index);
            }
        }

        return returnValue.OrderByDependencies();
    }

    Task<string> ICatalogFileReader.ReadTextFile(string catalogPath, int catalogId, string fileFilter, string? fileExtension)
    {
        ValidateParameters(catalogPath, catalogId);

        var dataPath = GetValidatedDataPath(catalogPath, catalogId);
        var indexPath = GetValidatedIndexPath(catalogPath, catalogId);

        return ReadTextFileInternal(indexPath, dataPath, fileFilter, fileExtension);
    }

    async IAsyncEnumerable<string> ICatalogFileReader.ReadTextFiles(IDictionary<string, IEnumerable<ICatalogIndexEntry>> entries, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var fileEntry in entries)
        {
            await using var stream = new FileStream(
                                                    fileEntry.Key,
                                                    FileMode.Open,
                                                    FileAccess.Read,
                                                    FileShare.Read,
                                                    bufferSize: 4096,
                                                    useAsync: true);

            foreach (var entry in fileEntry.Value)
            {
                VerifyReadLength(stream.Length, entry.Offset, entry.Size, fileEntry.Key);

                stream.Seek(entry.Offset, SeekOrigin.Begin);

                byte[] data = new byte[entry.Size];
                var read = await stream.ReadAsync(data.AsMemory(0, entry.Size), cancellationToken);

                VerifyReadSize(read, entry.Size, fileEntry.Key);

                yield return _encoding.GetString(data);
            }
        }
    }
}
