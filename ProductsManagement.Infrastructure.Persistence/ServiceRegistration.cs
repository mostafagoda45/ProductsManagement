using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsManagement.Application.Interfaces;
using ProductsManagement.Application.Interfaces.Repositories;
using ProductsManagement.Infrastructure.Persistence.Contexts;
using ProductsManagement.Infrastructure.Persistence.Repositories;
using ProductsManagement.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsManagement.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IVendorRepositoryAsync, VendorRepositoryAsync>();
            services.AddTransient(provider => new Lazy<IProductRepositoryAsync>(provider.GetService<IProductRepositoryAsync>()));
            services.AddTransient(provider => new Lazy<IVendorRepositoryAsync>(provider.GetService<IVendorRepositoryAsync>()));
            #endregion
        }
    }
}
