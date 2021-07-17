﻿namespace BuildingDrainageConsultant.Infrastructure
{
    using BuildingDrainageConsultant.Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<BuildingDrainageConsultantDbContext>();

            data.Database.Migrate();

            return app;
        }

        //private static void Seed
    }
}
