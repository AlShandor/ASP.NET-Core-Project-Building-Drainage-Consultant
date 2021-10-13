namespace BuildingDrainageConsultant.Data.Seeding.ExcelSeeders
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Merchants;
    using ExcelDataReader;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using System.Linq;

    using static DataConstants.ExcelSeeding;
    public class MerchantsExcelSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Merchants.Any())
            {
                return;
            }

            var merchantService = serviceProvider.GetRequiredService<IMerchantService>();

            var fileName = "wwwroot/data/merchantsData.xlsx";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read()) //Each row of the file
                    {
                        if (reader.Depth == 0) // skip Headers
                        {
                            continue;
                        }

                        var name = reader.GetValue(NameColumn) == null ? string.Empty : reader.GetValue(NameColumn).ToString().Trim();
                        var city = reader.GetValue(CityColumn) == null ? string.Empty : reader.GetValue(CityColumn).ToString().Trim();
                        var address = reader.GetValue(AddressColumn) == null ? string.Empty : reader.GetValue(AddressColumn).ToString().Trim();
                        var email = reader.GetValue(EmailColumn) == null ? string.Empty : reader.GetValue(EmailColumn).ToString().Trim();
                        var phone = reader.GetValue(PhoneColumn) == null ? string.Empty : reader.GetValue(PhoneColumn).ToString().Trim();
                        var website = reader.GetValue(WebsiteColumn) == null ? string.Empty : reader.GetValue(WebsiteColumn).ToString().Trim();
                        double? latitude = reader.GetValue(LatitudeColumn) == null ? null : double.Parse(reader.GetValue(LatitudeColumn).ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        double? longitude = reader.GetValue(LongitudeColumn) == null ? null : double.Parse(reader.GetValue(LongitudeColumn).ToString(), System.Globalization.CultureInfo.InvariantCulture);

                        var merchantData = new Merchant
                        {
                            Name = name,
                            City = city,
                            Address = address,
                            Email = email,
                            Phone = phone,
                            Website = website,
                            Latitude = latitude,
                            Longitude = longitude
                        };

                        dbContext.Merchants.Add(merchantData);
                        dbContext.SaveChanges();
                    }
                }
            }

        }

    }
}
