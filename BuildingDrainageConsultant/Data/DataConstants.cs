namespace BuildingDrainageConsultant.Data
{
    public static class DataConstants
    {
        public class User
        {
            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;
        }

        public class Drain
        {
            public const int NameMaxLength = 30;
            public const int DescriptionMaxLength = 1000;
            public const int FlowRateMax = 15;
            public const int DraingeAreaMax = 400;
            public const int DepthMax = 300;
            public const string DefaultImageUrl = "https://hl-bg.bg/components/com_virtuemart/assets/images/vmgeneral/noimage.gif";
        }

        public class AtticaDetail
        {
            public const int DescriptionMaxLength = 1000;
            public const string DefaultImageUrl = "https://hl-bg.bg/components/com_virtuemart/assets/images/vmgeneral/noimage.gif";
        }

        public class AtticaDrain
        {
            public const int NameMaxLength = 30;
            public const int FlowRateMax = 4;
            public const int DraingeAreaMax = 100;

        }

        public class AtticaPart
        {
            public const int NameMaxLength = 30;
            public const int DescriptionMaxLength = 1000;
            public const string DefaultImageUrl = "https://hl-bg.bg/components/com_virtuemart/assets/images/vmgeneral/noimage.gif";
        }

        public class Merchant
        {
            public const int NameMaxLength = 30;
            public const int CityMaxLength = 30;
            public const int AddressMaxLength = 300;
            public const int WebsiteMaxLength = 200;
            public const int EmailMaxLength = 100;
            public const int PhoneNumberMaxLength = 20;
        }

        public class Article
        {
            public const int TitleMaxLength = 60;
            public const int ContentMaxLength = 1000;
            public const string DefaultImageUrl = "https://hl-bg.bg/components/com_virtuemart/assets/images/vmgeneral/noimage.gif";
        }

        public class Image
        {
            public const int NameMaxLength = 100;
            public const int PathMaxLength = 255;
        }
    }
}