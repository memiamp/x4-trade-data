using System.Text;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services;

/// <summary>
/// A class that implements a X4 catalog file reader.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class CatalogFileReader(
                                 ILogger<CatalogFileReader> logger)
    : ICatalogFileReader
{
    private readonly Encoding _encoding = Encoding.UTF8;

    private static string GetDataPath(string catalogPath, int catalogId)
        => $"{GetPath(catalogPath, catalogId)}.{Constants.CatalogFile.FileExtensions.DataFile}";

    private async Task<CatalogIndexEntry> GetFirstIndexEntry(string indexPath, string? fileFilter)
    {
        var indexFiles = await ParseIndex(indexPath, fileFilter);
        if (!indexFiles.Any())
        {
            logger.LogWarning("The index file '{IndexFile}' contained no entries", indexPath);
            throw new ArgumentException("The specified index file contained no entries", nameof(indexPath)); 
        }

        var returnValue = indexFiles.FirstOrDefault();
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

    private CatalogIndexEntry ParseIndexEntry(string line, long offset)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 4 ||
            !int.TryParse(parts[1], out var fileSize) ||
            !long.TryParse(parts[2], out var unixTimestamp))
        {
            logger.LogWarning("The specified catalog index line '{Line}' was invalid", line);
            throw new ArgumentException("The specified catalog index line is invalid", nameof(line));
        }

        return new CatalogIndexEntry
        {
            FilePath = parts[0],
            Offset = offset,
            Signature = parts[3],
            Size = fileSize,
            Timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp)
        };
    }

    private async Task<IEnumerable<CatalogIndexEntry>> ParseIndex(string indexPath, string? fileFilter = null)
    {
        List<CatalogIndexEntry> returnValue = [];

        try
        {
            var offset = 0L;

            var lines = await File.ReadAllLinesAsync(indexPath);
            foreach (var line in lines)
            {
                var entry = ParseIndexEntry(line, offset);

                offset += entry.Size;

                if (fileFilter is null ||
                    line.Contains(fileFilter, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue.Add(entry);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unable to parse the index file {CatalogIndexFilePath}", indexPath);
        }

        return returnValue;
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

        if (stream.Length < offset + length)
        {
            logger.LogWarning("The specified length {Length} from {Offset} will exceed the size of {CatalogDataFilePath}", length, offset, dataPath);
            throw new ArgumentException("The specified offset and length exceed the data file size", nameof(offset));
        }

        stream.Seek(offset, SeekOrigin.Begin);

        byte[] returnValue = new byte[length];
        var read = await stream.ReadAsync(returnValue.AsMemory(0, length));

        if (read < length)
        {
            logger.LogWarning("Unable to read the requested {Length} from the data file {CatalogDataFilePath}", length, dataPath);
            throw new EndOfStreamException("Could not read the requested number of bytes.");
        }

        return returnValue;
    }

    private static bool ValidateCatalogId(int catalogId)
        => catalogId > 0 && catalogId <= 99;

    private async Task<string> ReadTextFileInternal(string indexPath, string dataPath, string fileFilter)
    {
        logger.LogInformation("Reading the first text file matching '{FileFilter}' from catalog {CatalogDataFilePath}", fileFilter, dataPath);

        var indexFile = await GetFirstIndexEntry(indexPath, fileFilter);

        var data = await ReadFromDataFile(dataPath, indexFile.Offset, indexFile.Size);

        return _encoding.GetString(data);
    }

    Task<string> ICatalogFileReader.ReadTextFile(string catalogPath, int catalogId, string fileFilter)
    {
        if (!Path.Exists(catalogPath))
        {
            logger.LogWarning("The catalog path {CatalogPath} is invalid", catalogPath);
            throw new ArgumentException("The specified catalog path is invalid", nameof(catalogPath));
        }

        if (!ValidateCatalogId(catalogId))
        {
            logger.LogWarning("The catalog identifier {CatalogId} is invalid", catalogId);
            throw new ArgumentException("The specified catalog identifier is invalid", nameof(catalogId));
        }

        var dataFilePath = GetDataPath(catalogPath, catalogId);
        var indexFilePath = GetIndexPath(catalogPath, catalogId);

        if (!File.Exists(dataFilePath))
        {
            logger.LogWarning("The catalog data file {CatalogDataFilePath} does not exist", dataFilePath);
            throw new ArgumentException("The catalog data file does not exist", nameof(catalogId));
        }

        if (!File.Exists(indexFilePath))
        {
            logger.LogWarning("The catalog index file {CatalogIndexFilePath} does not exist", indexFilePath);
            throw new ArgumentException("The catalog index file does not exist", nameof(catalogId));
        }

        return ReadTextFileInternal(indexFilePath, dataFilePath, fileFilter);
    }
}
