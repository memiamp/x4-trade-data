namespace MPL.X4;

/// <summary>
/// A class that defines X4 constants.
/// </summary>
public static class Constants
{
    /// <summary>
    /// Constants relating to X4 resource files.
    /// </summary>
    public static class ResourceFile
    {
        public static class AttributeName
        {
            public const string PageId = "id";
            public const string TextId = "id";
        }

        public static class ElementName
        {
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
            public const string Id = "id";
            public const string Known = "known";
            public const string KnownTo = "knownto";
            public const string Macro = "macro";
            public const string Owner = "owner";
            public const string Seller = "seller";
            public const string Price = "price";
            public const string Ware = "ware";
            public const string X = "x";
            public const string Y = "y";
            public const string Z = "z";
        }

        public static class AttributeValue
        {
            public static class Class
            {
                public const string Sector = "sector";
                public const string Ship = "ship";
                public const string Station = "station";
                public const string Zone = "zone";
            }

            public static class KnownTo
            {
                public const string Player = "player";
            }

            public static class  Owner
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
            public const string Position = "position";
            public const string Trade = "trade";
            public const string Universe = "universe";
        }
    }
}
