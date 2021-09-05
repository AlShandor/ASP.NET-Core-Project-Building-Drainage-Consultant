namespace BuildingDrainageConsultant.Data.Seeding
{
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Services.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public class ExtensionsSeeder : ISeeder
    {
        public void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Extensions.Any())
            {
                return;
            }

            var extensionService = serviceProvider.GetRequiredService<IExtensionService>();

            var extensions = new Extension[]
            {
                new Extension
                {
                    Name = "HL340N",
                    ImageId = 1,
                    Description = "Удължителен елемент d 110мм, с височина 80 мм, О-ринг гумен уплътнител."
                },
                new Extension
                {
                    Name = "HL350",
                    ImageId = 1,
                    Description = "Удължителен елемент d 145мм/155мм за серии HL62,HL64,HL72,HL317"
                },
                new Extension
                {
                    Name = "HL3400",
                    ImageId = 1,
                    Description = "Удължител d 146 mm, h=200 mm, вкл. O-Ring, за сериите HL3100, HL5100"
                },
            };

            extensionService.CreateAll(extensions);
        }
    }
}
