using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using Microsoft.Extensions.Logging;
using MPL.X4.Services.Models;

namespace MPL.X4.Services;

/// <summary>
/// A class that implements a loader of save games.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
/// <param name="xmlReaderFactory">An <see cref="IXmlReaderWrapperFactory"/> that is the XmlReader factory to use.</param>
internal class SaveGameLoader(
                              ILogger<SaveGameLoader> logger,
                              IXmlReaderWrapperFactory xmlReaderFactory)
    : ISaveGameLoader
{

    private const string ClusterClusterText = "cluster_";
    private const string ClusterSectorText = "_sector";

    private int GetSectorNameIdFromMacro(string source)
    {
        if (source.Length > 9)
        {
            var clusterEndPos = source.IndexOf('_', ClusterClusterText.Length + 1);
            var sectorStartPos = source.IndexOf(ClusterSectorText);
            var sectorEndPos = source.IndexOf('_', sectorStartPos + 1);

            if (clusterEndPos >= 0 &&
                sectorStartPos >= 0 &&
                sectorEndPos >= 0)
            {
                var clusterText = source[ClusterClusterText.Length..source.IndexOf('_', ClusterClusterText.Length)];
                var sectorText = source[(sectorStartPos + ClusterSectorText.Length)..sectorEndPos];

                var sectorIdText = $"{clusterText}{sectorText}";
                if (int.TryParse(sectorIdText, out var returnValue))
                {
                    return returnValue;
                }
            }
        }

        logger.LogWarning("Cannot parse sector identifier from {SourceValue}", source);
        throw new ArgumentException($"Cannot parse sector identifier from {source}", nameof(source));
    }

    private static bool IsKnownToPlayer(IXmlReaderWrapper reader)
        => reader.TryGetAttribute(
                                  Constants.SaveGameFile.AttributeName.KnownTo,
                                  x => x == Constants.SaveGameFile.AttributeValue.KnownTo.Player);

    private async Task<ILockbox> LoadLockbox(IXmlReaderWrapper reader)
    {
        ILockbox? returnValue = null;
        var position = new SectorPosition
        {
            X = 0,
            Y = 0,
            Z = 0
        };

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Lockbox) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? type))
            {
                var isKnown = IsKnownToPlayer(reader);

                returnValue = new Lockbox
                {
                    Code = code,
                    Id = id,
                    IsKnown = isKnown,
                    Position = position,
                    Type = type
                };
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Position, XmlNodeType.Element))
            {
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.X, out double? x);
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Y, out double? y);
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Z, out double? z);

                position.X = (int)(x ?? 0);
                position.Y = (int)(y ?? 0);
                position.Z = (int)(z ?? 0);
            }
        }

        if (returnValue is null)
        {
            logger.LogWarning("Could not load lockbox");
            throw new ArgumentException("Could not load lockbox", nameof(reader));
        }

        return returnValue;
    }

    private async Task<ISector> LoadSector(IXmlReaderWrapper reader)
    {
        ISector? returnValue = null;
        List<ILockbox> lockboxes = [];
        List<IShip> ships = [];
        List<IStation> stations = [];

        if (await reader.ReadAsync() &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? macro) &&
            reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, out string? owner))
        {
            var isKnown = IsKnownToPlayer(reader);
            var nameId = GetSectorNameIdFromMacro(macro);

            returnValue = new Sector
            {
                Code = code,
                Id = id,
                IsKnown = isKnown,
                Lockboxes = lockboxes,
                NameId = nameId,
                Owner = owner,
                Ships = ships,
                Stations = stations
            };

            while (await reader.ReadAsync())
            {
                if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                    reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Zone))
                {
                    using var zoneSubtree = reader.ReadSubtree();

                    await LoadZone(zoneSubtree, lockboxes, ships, stations);
                }
            }
        }

        if (returnValue is null)
        {
            logger.LogWarning("Could not load sector");
            throw new ArgumentException("Could not load sector", nameof(reader));
        }

        return returnValue;
    }
    List<string> donePostIds = [];

    private async Task<IEnumerable<IShip>> LoadShips(IXmlReaderWrapper reader, ISectorPosition position)
    {
        List<IShip> returnValue = [];
        Ship? currentShip = null;

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x.StartsWith(Constants.SaveGameFile.AttributeValue.Class.Ship), out string? shipClass) &&
                //reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, x => x == Constants.SaveGameFile.AttributeValue.Owner.Ownerless, out string? owner) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, out string? owner) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Macro, out string? macro))
            {
                var isKnown = IsKnownToPlayer(reader);

                //returnValue.Add(new Ship
                currentShip = new Ship
                {
                    Class = shipClass,
                    Code = code,
                    Id = id,
                    IsKnown = isKnown,
                    Macro = macro,
                    Owner = owner,
                    Position = position
                };
                //});
                if (owner == "ownerless")
                {
                    Console.WriteLine("ABANDONED");
                    returnValue.Add(currentShip);
                    currentShip = null;
                }
                /*
								<control>
									<post id="aipilot" component="[0xc081993]"/>
								</control>
                * */
            }
            else if (reader.CheckNodeMatches("control", XmlNodeType.Element))
            {
                if (currentShip is not null)
                {
                    // Look for post
                    await reader.ReadAsync();
                    if (reader.CheckNodeMatches("post", XmlNodeType.Element))
                    {
                        reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? postId);
                        reader.TryGetAttribute("component", out string? component);

                        if (postId is null)
                        {
                            Console.WriteLine("PostID Is null");
                        }
                        else if (postId == "aipilot" && component is null)
                        {
                            Console.WriteLine("Ship without pilot component");
                            returnValue.Add(currentShip);
                            currentShip = null;
                        }
                        else if (!donePostIds.Contains(postId))
                        {
                            donePostIds.Add(postId);
                            Console.WriteLine($"{postId} - component: {component ?? "NONE"}");
                        }
                    }
                }
            }
        }

        return returnValue;
    }

    private async Task<IStation> LoadStation(IXmlReaderWrapper reader, ISectorPosition position)
    {
        IStation? returnValue = null;
        List<ITrade> trades = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Station) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Code, out string? code) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, out string? owner))
            {
                var isKnown = IsKnownToPlayer(reader);

                var nameId = reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.BaseName, out string? baseName)
                    ? new TextResourceReference(baseName)
                    : null;

                returnValue = new Station
                {
                    Code = code,
                    Id = id,
                    IsKnown = isKnown,
                    NameId = nameId,
                    Owner = owner,
                    Position = position,
                    Trades = trades
                };
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Trade, XmlNodeType.Element) &&
                     !reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? _))
            {
                // Primary trade element has no ID attribute
                using var tradeSubtree = reader.ReadSubtree();

                var loadedTrades = await LoadTrades(tradeSubtree);
                trades.AddRange(loadedTrades);
            }
        }

        if (returnValue is null)
        {
            logger.LogWarning("Could not load station");
            throw new ArgumentException("Could not load station", nameof(reader));
        }

        return returnValue;
    }

    private static async Task<IEnumerable<ITrade>> LoadTrades(IXmlReaderWrapper reader)
    {
        List<ITrade> returnValue = [];

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Trade, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Amount, out int? amount) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Id, out string? id) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Price, out int? price) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Ware, out string? ware))
            {
                var amountToBuy = 0;
                var amountToSell = 0;

                if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Buyer, out string? _))
                {
                    amountToBuy = amount.Value;
                }

                if (reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Seller, out string? _))
                {
                    amountToSell = amount.Value;
                }

                returnValue.Add(new Trade
                {
                    AmountToBuy = amountToBuy,
                    AmountToSell = amountToSell,
                    Id = id,
                    Price = price.Value,
                    Ware = ware
                });
            }
        }

        return returnValue;
    }

    private async Task<IUniverse> LoadUniverse(IXmlReaderWrapper reader)
    {
        var sectors = new List<ISector>();

        while (await reader.ReadAsync())
        {
            // Check for sector
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Sector))
            {
                using var sectorSubtree = reader.ReadSubtree();

                var sector = await LoadSector(sectorSubtree);

                sectors.Add(sector);
            }
        }

        return new Universe
        {
            Sectors = sectors
        };
    }

    private async Task LoadZone(IXmlReaderWrapper reader, List<ILockbox> lockboxes, List<IShip> ships, List<IStation> stations)
    {
        ISectorPosition? position = ISectorPosition.GetDefault();
   
        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Position, XmlNodeType.Element))
            {
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.X, out int? x);
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Y, out int? y);
                reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Z, out int? z);

                position = new SectorPosition
                {
                    X = x ?? 0,
                    Y = y ?? 0,
                    Z = z ?? 0
                };
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Station))
            {
                using var stationSubtree = reader.ReadSubtree();

                var station = await LoadStation(stationSubtree, position);

                stations.Add(station);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x == Constants.SaveGameFile.AttributeValue.Class.Lockbox))
            {
                using var lockboxSubtree = reader.ReadSubtree();

                var lockbox = await LoadLockbox(lockboxSubtree);

                lockboxes.Add(lockbox);
            }
            else if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Component, XmlNodeType.Element) &&
                     reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x.StartsWith(Constants.SaveGameFile.AttributeValue.Class.Ship), out string? shipClass))
            //reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Class, x => x.StartsWith(Constants.SaveGameFile.AttributeValue.Class.Ship), out string? shipClass) &&
            //reader.TryGetAttribute(Constants.SaveGameFile.AttributeName.Owner, x => x == Constants.SaveGameFile.AttributeValue.Owner.Ownerless, out string? _))
            {
                // Look for abandoned ships in zone
                using var shipSubtree = reader.ReadSubtree();

                var abandonedShips = await LoadShips(shipSubtree, position);

                ships.AddRange(abandonedShips);
            }
        }
    }

        async Task<ISaveGame> ISaveGameLoader.LoadFrom(string sourcePath)
    {
        ISaveGame? returnValue = null;

        using var reader = xmlReaderFactory.CreateXmlReader(sourcePath);

        while (await reader.ReadAsync())
        {
            if (reader.CheckNodeMatches(Constants.SaveGameFile.ElementName.Universe, XmlNodeType.Element))
            {
                using var universeSubtree = reader.ReadSubtree();

                var universe = await LoadUniverse(universeSubtree);
                returnValue = new SaveGame
                {
                    Universe = universe
                };

                break;
            }
        }

        if (returnValue is null)
        {
            logger.LogWarning("Could not load save game from {SourcePath}", sourcePath);
            throw new ArgumentException($"Could not load save game from '{sourcePath}'", nameof(sourcePath));
        }

        return returnValue;
    }
}
