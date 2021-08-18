namespace BuildingDrainageConsultant.Data.Seeding
{
    using System;

    public interface ISeeder
    {
        void Seed(BuildingDrainageConsultantDbContext dbContext, IServiceProvider serviceProvider);
    }
}
