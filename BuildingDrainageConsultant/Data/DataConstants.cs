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
    }
}