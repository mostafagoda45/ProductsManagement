using Microsoft.EntityFrameworkCore;
using ProductsManagement.Application.Interfaces;
using ProductsManagement.Application.Interfaces.Repositories;
using ProductsManagement.Domain.Entities;
using ProductsManagement.Infrastructure.Persistence.Contexts;
using ProductsManagement.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductRepositoryAsync(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private IApplicationDbContext ApplicationDbContext => _applicationDbContext;

        public Task<List<Product>> GetAllProductsListAsync(string title, string description, int pageNumber = 1, int pageSize = 10)
        {
            return ApplicationDbContext.Products
                .Where(c => title == null || c.Title.ToLower().Contains(title.ToLower()))
                .Where(c => description == null || c.Description.ToLower().Contains(description.ToLower()))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<bool> IsUniqueProductAsync(string createdProductHash)
        {
            return Task.FromResult(!ApplicationDbContext.Products.AnyAsync(c => c.ProductHash == createdProductHash).Result);
        }
    }
}
