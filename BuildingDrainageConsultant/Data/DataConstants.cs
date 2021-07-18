﻿namespace BuildingDrainageConsultant.Data
{
    public static class DataConstants
    {

        public class Drain
        {
            public const int NameMaxLength = 30;
            public const int DescriptionMaxLength = 1000;
            public const int FlowRateMax = 15;
            public const int DraingeAreaMax = 400;
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

        public class Seller
        {
            public const int NameMaxLength = 30;
            public const int CityMaxLength = 30;
            public const int AddressMaxLength = 300;
            public const int WebsiteMaxLength = 200;
            public const int EmailMaxLength = 100;
        }

    }
}
