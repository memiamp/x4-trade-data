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
        public const string BaseGame = "BaseGame";
        public const string GameExtension = "Extension";

        public static class DirectoryName
        {
            public const string Extensions = "extensions";
        }

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
            public const string FactionsXml = "libraries/factions.xml";
            public const string LandmarkMacros = "landmarks/macros/";
            public const string MapDefinitionXml = "mapdefaults.xml";
            public static readonly string[] ShipMacros = [ShipMacrosL, ShipMacrosM, ShipMacrosS, ShipMacrosXL, ShipMacrosXS];
            public const string ShipMacrosL = "assets/units/size_l/macros/";
            public const string ShipMacrosM = "assets/units/size_m/macros/";
            public const string ShipMacrosS = "assets/units/size_s/macros/";
            public const string ShipMacrosXL = "assets/units/size_xl/macros/";
            public const string ShipMacrosXS = "assets/units/size_xs/macros/";
            public const string SignatureIndicator = "_sig";
            public const string WareDefinition = "libraries/wares.xml";
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
    /// Constants relating to X4 catalog files.
    /// </summary>
    public static class ContentFile
    {
        public static class AttributeName
        {
            public const string ContentId = "id";
            public const string DependencyId = "id";
        }

        public static class ElementName
        {
            public const string Content = "content";
            public const string Dependency = "dependency";
        }

        public static class FileName
        {
            public const string Data = "content.xml";
            public const string Signature = "content.xml.sig";
        }
    }

    /// <summary>
    /// Constants relating to file extensions
    /// </summary>
    public static class FileExtensions
    {
        public const string GzipFile = ".gz";
    }

    /// <summary>
    /// Constants relating to text resourcezs.
    /// </summary>
    public static class TextResource
    {
        public const char SplitChar = ',';
        public static readonly char[] TrimChars = ['{', '}'];
    }

    /// <summary>
    /// Constants relation to X4 XML data files.
    /// </summary>
    public static class XmlDataFile
    {
        public static class AttributeName
        {
            public const string Amount = "amount";
            public const string BaseName = "basename";
            public const string BoostAcceleration = "boostacc";
            public const string BuildAnchorId = "id";
            public const string BuildingModuleId = "id";
            public const string BoostDuration = "boostduration";
            public const string BoostThrust = "boostthrust";
            public const string BuildStorageId = "id";
            public const string Buy = "buy";
            public const string Buyer = "buyer";
            public const string Capacity = "capacity";
            public const string ChargeTime = "chargetime";
            public const string Class = "class";
            public const string Code = "code";
            public const string ColourAlpha = "a";
            public const string ColourBlue = "b";
            public const string ColourGlow = "glow";
            public const string ColourGreen = "g";
            public const string ColourId = "id";
            public const string ColourRed = "r";
            public const string Connection = "connection";
            public const string Cooling = "cooling";
            public const string CountermeasureCapacity = "countermeasurecapacity";
            public const string Damage = "damage";
            public const string Default = "default";
            public const string DeployableCapacity = "deployablecapacity";
            public const string Desired = "desired";
            public const string Description = "descr";
            public const string Drag = "drag";
            public const string Exact = "exact";
            public const string FactionId = "id";
            public const string FactoryName = "factoryname";
            public const string ForwardThrust = "forwardthrust";
            public const string GateId = "id";
            public const string Generated = "generated";
            public const string Group = "group";
            public const string KnownTo = "knownto";
            public const string Lifetime = "lifetime";
            public const string LockboxId = "id";
            public const string Macro = "macro";
            public const string MappingId = "id";
            public const string Mass = "mass";
            public const string MaximumHull = "maxhull";
            public const string Mining = "mining";
            public const string MissileCapacity = "missilecapacity";
            public const string Name = "name";
            public const string NameIndex = "nameindex";
            public const string Owner = "owner";
            public const string PageId = "id";
            public const string Pitch = "pitch";
            public const string Price = "price";
            public const string QW = "qw";
            public const string QX = "qx";
            public const string QY = "qy";
            public const string QZ = "qz";
            public const string RadarCloak = "radarcloak";
            public const string RadarRange = "radarrange";
            public const string RechargeDelay = "rechargedelay";
            public const string RechargeRate = "rechargerate";
            public const string RegionDamage = "regiondamage";
            public const string Reference = "ref";
            public const string Reload = "reload";
            public const string Roll = "roll";
            public const string RotationSpeed = "rotationspeed";
            public const string RotationThrust = "rotationthrust";
            public const string SectorId = "id";
            public const string Sell = "sell";
            public const string Seller = "seller";
            public const string ShipId = "id";
            public const string ShortName = "shortname";
            public const string State = "state";
            public const string StationId = "id";
            public const string Sticktime = "sticktime";      
            public const string StrafeAcceleration = "strafeacc";
            public const string StrafeThrust = "strafethrust";
            public const string SurfaceElement = "surfaceelement";      
            public const string TextId = "id";
            public const string Title = "title";
            public const string TradeId = "id";
            public const string Transport = "transport";
            public const string TravelAttackTime = "travelattacktime";
            public const string TravelChargeTime = "travelchargetime";
            public const string TravelStartThrust = "travelstartthrust";
            public const string TravelThrust = "travelthrust";
            public const string UnitCapacity = "unitcapacity";
            public const string Volume = "volume";
            public const string Ware = "ware";
            public const string WareId = "id";
            public const string X = "x";
            public const string Y = "y";
            public const string Yaw = "yaw";
            public const string Z = "z";
            public const string ZoneId = "id";
        }

        public static class AttributeValue
        {
            public static class Class
            {
                public const string BuildModule = "buildmodule";
                public const string BuildProcessor = "buildprocessor";
                public const string BuildStorage = "buildstorage";
                public const string CollectableAmmo = "collectableammo";
                public const string CollectableWares = "collectablewares";
                public const string DefenceModule = "defencemodule";
                public const string Gate = "gate";
                public const string Lock = "lock";
                public const string Lockbox = "lockbox";
                public const string Production = "production";
                public const string Sector = "sector";
                public const string Ship = "ship";
                public const string ShipExtraLarge = "ship_xl";
                public const string ShipExtraSmall = "ship_xs";
                public const string ShipLarge = "ship_l";
                public const string ShipMedium = "ship_m";
                public const string ShipSmall = "ship_s";
                public const string Station = "station";
                public const string Storage = "storage";
                public const string Zone = "zone";
            }

            public static class Connection
            {
                public const string BuildAnchor = "buildanchor";
                public const string BuildModule = "con_buildmodule";
                public const string BuildingModule = "buildingmodule";
                public const string Drops = "drops";
                public const string Storage = "con_storage";
            }

            public static class KnownTo
            {
                public const string Player = "player";
            }

            public static class Owner
            {
                public const string Ownerless = "ownerless";
            }

            public static class Reference
            {
                public const string Sectors = "sectors";
                public const string Zones = "zones";
            }

            public static class State
            {
                public const string Construction = "construction";
                public const string Wreck = "wreck";
            }

            public static class WareTransport
            {
                public const string Container = "container";
                public const string Equipment = "equipment";
                public const string Inventory = "inventory";
                public const string Liquid = "liquid";
                public const string Ship = "ship";
                public const string Solid = "solid";
            }
        }

        public static class ElementName
        {
            public const string Ammunition = "ammunition";
            public const string Cargo = "cargo";
            public const string Colour = "color";
            public const string Colours = "colors";
            public const string Component = "component";
            public const string Connected = "connected";
            public const string Connection = "connection";
            public const string Connections = "connections";
            public const string Dataset = "dataset";
            public const string Defaults = "defaults";
            public const string Engine = "engine";
            public const string Faction = "faction";
            public const string Factions = "factions";
            public const string Identification = "identification";
            public const string Macro = "macro";
            public const string Macros = "macros";
            public const string Mapping = "mapping";
            public const string Mappings = "mappings";
            public const string Modification = "modification";
            public const string Offset = "offset";
            public const string Page = "page";
            public const string Paint = "paint";
            public const string Position = "position";
            public const string Production = "production";
            public const string Quaternion = "quaternion";
            public const string Queue = "queue";
            public const string Rotation = "rotation";
            public const string Shields = "shields";
            public const string Ship = "ship";
            public const string TextEntry = "t";
            public const string Trade = "trade";
            public const string Universe = "universe";
            public const string Ware = "ware";
            public const string Wares = "wares";
        }

        public static class XPath
        {
            public static class BuildStorage
            {
                public const string BuildAnchorConnection = $"//component[@class='{AttributeValue.Class.BuildModule}']//component[@class='{AttributeValue.Class.BuildProcessor}']//connection[@connection='{AttributeValue.Connection.BuildAnchor}']";
                public const string BuildAnchorConnected =  $"{BuildAnchorConnection}/connected";
            }
            }
        }
}
