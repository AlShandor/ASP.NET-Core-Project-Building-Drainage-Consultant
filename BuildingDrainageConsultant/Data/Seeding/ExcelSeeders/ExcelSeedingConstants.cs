namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{

    public static class ExcelSeedingConstants
    {
        public class Drains
        {
            public const int NameColumn = 0;
            public const int DescriptionColumn = 1;
            public const int FlowRateColumn = 2;
            public const int DrainageAreaColumn = 3;
            public const int DepthColumn = 4;
            public const int DirectionColumn = 5;
            public const int DiameterColumn = 6;
            public const int VisiblePartColumn = 7;
            public const int WaterproofingColumn = 8;
            public const int HeatingColumn = 9;
            public const int RenovationColumn = 10;
            public const int FlapSealColumn = 11;
            public const int LoadClassColumn = 12;
            public const int ImageColumn = 13;
            public const int WaterproofingKitColumn = 14;
            public const int AccessoriesColumn = 15;
        }

        public class Attica
        {
            public const int NameColumn = 0;
            public const int DescriptionColumn = 1;
            public const int AtticaPartsColumn = 0;
            public const int AtticaPartImageNameColumn = 2;
            public const int AtticaDetailIdColumn = 1;
            public const int WalkableColumn = 2;
            public const int RoofTypeColumn = 3;
            public const int ScreedWaterproofingColumn = 4;
            public const int ConcreteWaterproofingColumn = 5;
            public const int DiameterColumn = 6;
            public const int VisiblePartColumn = 7;
            public const int FlowRate35mmColumn = 8;
            public const int FlowRate100mmColumn = 9;
            public const int DrainageArea35mmColumn = 10;
            public const int DrainageArea100mmColumn = 11;
        }

        public class Merchants
        {
            public const int NameColumn = 0;
            public const int CityColumn = 1;
            public const int AddressColumn = 2;
            public const int EmailColumn = 3;
            public const int PhoneColumn = 4;
            public const int WebsiteColumn = 5;
            public const int LatitudeColumn = 6;
            public const int LongitudeColumn = 7;
        }
    }
}
