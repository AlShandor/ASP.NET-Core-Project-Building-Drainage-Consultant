namespace BuildingDrainageConsultant.Data
{
    public static class DataConstants
    {
        public class User
        {
            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 4;
        }

        public class Drain
        {
            public const int NameMaxLength = 60;
            public const int DescriptionMaxLength = 5000;
            public const int FlowRateMax = 15;
            public const int DraingeAreaMax = 400;
            public const int DepthMax = 300;
            public const int DefaultImageId = 1;
        }

        public class AtticaDetail
        {
            public const int DescriptionMaxLength = 3000;
            public const int DefaultImageId = 1;
        }

        public class AtticaDrain
        {
            public const int NameMaxLength = 50;
            public const int FlowRateMax = 6;
            public const int DraingeAreaMax = 200;

        }

        public class AtticaPart
        {
            public const int NameMaxLength = 50;
            public const int DescriptionMaxLength = 2000;
            public const int DefaultImageId = 1;
        }

        public class Merchant
        {
            public const int NameMaxLength = 50;
            public const int CityMaxLength = 30;
            public const int AddressMaxLength = 1000;
            public const int WebsiteMaxLength = 200;
            public const int EmailMaxLength = 100;
            public const int PhoneNumberMaxLength = 70;
        }

        public class Article
        {
            public const int TitleMaxLength = 60;
            public const int ContentMaxLength = 6000;
            public const int DefaultImageId = 1;
        }

        public class Image
        {
            public const int NameMaxLength = 100;
            public const int PathMaxLength = 255;
        }

        public class WaterproofingKit
        {
            public const int NameMaxLength = 50;
            public const int DescriptionMaxLength = 2000;
            public const int DefaultImageId = 1;
        }

        public class Accessory
        {
            public const int NameMaxLength = 50;
            public const int DescriptionMaxLength = 2000;
            public const int DefaultImageId = 1;
        }

        public class Extension
        {
            public const int NameMaxLength = 50;
            public const int DescriptionMaxLength = 2000;
            public const int DefaultImageId = 1;
        }

        public class ExcelSeeding
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

            // Attica
            public const int WalkableColumn = 2;
            public const int RoofTypeColumn = 3;
            public const int ScreedWaterproofingColumn = 4;
            public const int AtticaPartImageNameColumn = 2;

            // Merchants
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