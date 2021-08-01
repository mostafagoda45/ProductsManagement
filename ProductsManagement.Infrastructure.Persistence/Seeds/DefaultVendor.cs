using ProductsManagement.Domain.Entities;
using ProductsManagement.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Infrastructure.Persistence.Seeds
{
    public static class DefaultVendor
    {
        public static async Task SeedAsync(ApplicationDbContext applicationDbContext)
        {
            if(!applicationDbContext.Vendors.Any())
            {
                //Seed Vendor
                await applicationDbContext.Vendors.AddAsync(new Vendor() { Name = "Azure", Address = "USA", ContactNumber = "1-855-270-0615" });
                await applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
