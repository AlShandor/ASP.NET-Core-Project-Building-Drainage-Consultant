namespace BuildingDrainageConsultant
{
    using BuildingDrainageConsultant.Data;
    using BuildingDrainageConsultant.Data.Models;
    using BuildingDrainageConsultant.Infrastructure;
    using BuildingDrainageConsultant.Services.Accessories;
    using BuildingDrainageConsultant.Services.Articles;
    using BuildingDrainageConsultant.Services.AtticaDetail;
    using BuildingDrainageConsultant.Services.AtticaDrains;
    using BuildingDrainageConsultant.Services.AtticaParts;
    using BuildingDrainageConsultant.Services.Drains;
    using BuildingDrainageConsultant.Services.Extensions;
    using BuildingDrainageConsultant.Services.Images;
    using BuildingDrainageConsultant.Services.Merchants;
    using BuildingDrainageConsultant.Services.WaterproofingKits;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BuildingDrainageConsultantDbContext>(options => options
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options => 
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BuildingDrainageConsultantDbContext>();

            services.AddAutoMapper(typeof(Startup));

            services.AddMemoryCache();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IDrainService, DrainService>();
            services.AddTransient<IMerchantService, MerchantService>();
            services.AddTransient<IAtticaPartService, AtticaPartService>();
            services.AddTransient<IAtticaDetailService, AtticaDetailService>();
            services.AddTransient<IAtticaDrainService, AtticaDrainService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IImageHLService, ImageHLService>();
            services.AddTransient<IWaterproofingKitService, WaterproofingKitService>();
            services.AddTransient<IAccessoryService, AccessoryService>();
            services.AddTransient<IExtensionService, ExtensionService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.SetCultureInfo("en-US");
            

            app.UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                        name: "Admin",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}