using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductsManagement.Application.Interfaces;
using ProductsManagement.Infrastructure.Identity.Contexts;
using ProductsManagement.Infrastructure.Identity.Models;
using ProductsManagement.Infrastructure.Identity.Seeds;
using ProductsManagement.Infrastructure.Persistence.Contexts;
using ProductsManagement.Infrastructure.Persistence.Seeds;
using Serilog;
using System;
using System.Threading.Tasks;

namespace ProductsManagement.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            //Read Configuration from appSettings
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    using var dbContext = (ApplicationDbContext)services.GetService<IApplicationDbContext>();
                    dbContext.Database.Migrate();

                    using var identityDbContext = services.GetService<IdentityContext>();
                    identityDbContext.Database.Migrate();

                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    await DefaultRoles.SeedAsync(userManager, roleManager);
                    await DefaultAdmin.SeedAsync(userManager, roleManager);
                    await DefaultBasicUser.SeedAsync(userManager, roleManager);
                    await DefaultVendor.SeedAsync(dbContext);
                    Log.Information("Finished Seeding Default Data");
                    Log.Information("Application Starting");
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "An error occurred seeding the DB");
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
            host.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
            .UseStartup<Startup>();
    }
}
