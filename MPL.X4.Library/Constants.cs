namespace MPL.X4;

/// <summary>
/// A class that defines X4 constants.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Constants relating to X4 catalog files.
    /// </summary>
    public static class CatalogFile
    {
        public static class FileExtensions
        {
            public const string DataFile = "dat";
            public const string IndexFile = "cat";
            public const string XmlData = "xml";
        }

        public static class FileId
        {
            public const int GameData = 8;
            public const int TextResources = 9;
        }

        public static class FileName
        {
            public const string ColourXml = "colors.xml";
            public const string EnglishTextResource = $"{LanguageId.English}.{FileExtensions.XmlData}";
            public const string FactionsXml = "factions.xml";
            public const string MapDefinitionXml = "mapdefaults.xml";
        }

        public static class IndexFile
        {
            public const string ColumnSeparator = " ";
        }

        public static class LanguageId
        {
            public const string English = "44";
        }
    }

    /// <summary>
    /// Constants relating to X4 resource files.
    /// </summary>
    public static class ResourceFile
    {
        public static class AttributeName
        {
            public const string ColourAlpha = "a";
            public const string ColourBlue = "b";
            public const string ColourGlow = "glow";
            public const string ColourGreen = "g";
            public const string ColourId = "id";
            public const string ColourRed = "r";
            public const string FactionId = "id";
            public const string Macro = "macro";
            public const string MappingId = "id";
            public const string Name = "name";
            public const string PageId = "id";
            public const string Ref = "ref";
            public const string ShortName = "shortname";
            public const string TextId = "id";
        }

        public static class AttributeValue
        {
            public static class Ref
            {
                public const string Zones = "zones";
            }
        }

        public static class ElementName
        {
            public const string Colour = "color";
            public const string Colours = "colors";
            public const string Connection = "connection";
            public const string Dataset = "dataset";
            public const string Faction = "faction";
            public const string Identification = "identification";
            public const string Macro = "macro";
            public const string Mapping = "mapping";
            public const string Mappings = "mappings";
            public const string Offset = "offset";
            public const string Page = "page";
            public const string TextEntry = "t";
        }

        public static class PageId
        {
            public const int Landmarks = 20103;
            public const int SectorNames = 20004;
            public const int StationNames = 20102;
            public const int WareGroups = 20215;
            public const int Wares = 20201;
        }
    }

    /// <summary>
    /// Constants relating to X4 save game files.
    /// </summary>
    public static class SaveGameFile
    {
        public const string BooleanYes = "1";

        public static class AttributeName
        {
            public const string Amount = "amount";
            public const string BaseName = "basename";
            public const string Buyer = "buyer";
            public const string Class = "class";
            public const string Code = "code";
            public const string Connection = "connection";
            public const string Desired = "desired";
            public const string Id = "id";
            public const string Known = "known";
            public const string KnownTo = "knownto";
            public const string Macro = "macro";
            public const string Owner = "owner";
            public const string Ref = "ref";
            public const string Seller = "seller";
            public const string Pitch = "pitch";
            public const string Price = "price";
            public const string Roll = "roll";
            public const string Ware = "ware";
            public const string X = "x";
            public const string Y = "y";
            public const string Yaw = "yaw";
            public const string Z = "z";
        }

        public static class AttributeValue
        {
            public static class Class
            {
                public const string Gate = "gate";
                public const string Lock = "lock";
                public const string Lockbox = "lockbox";
                public const string Sector = "sector";
                public const string Ship = "ship";
                public const string Station = "station";
                public const string Zone = "zone";
            }

            public static class KnownTo
            {
                public const string Player = "player";
            }

            public static class Owner
            {
                public const string Ownerless = "ownerless";
            }

            public static class ShipClass
            {
                public const string ExtraLarge = "ship_xl";
                public const string Large = "ship_l";
                public const string Medium = "ship_m";
                public const string Small = "ship_s";
            }
        }

        public static class ElementName
        {
            public const string Component = "component";
            public const string Connection = "connection";
            public const string Macro = "macro";
            public const string Offset = "offset";
            public const string Position = "position";
            public const string Production = "production";
            public const string Rotation = "rotation";
            public const string Trade = "trade";
            public const string Universe = "universe";
        }
    }

    /// <summary>
    /// Constants relation to X4 XML data files.
    /// </summary>
    public static class XmlDataFile
    {
        public static class AttributeName
        {
            public const string Amount = "amount";
            public const string Ware = "ware";
        }

        public static class ElementName
        {
            public const string Cargo = "cargo";
            public const string Engine = "engine";
            public const string Faction = "faction";
            public const string Factions = "factions";
            public const string Modification = "modification";
            public const string Paint = "paint";
            public const string Ship = "ship";
            public const string Ware = "ware";
            public const string Wares = "wares";
        }
    }
}
