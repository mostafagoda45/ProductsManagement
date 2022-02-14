using ProductsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
    {
        Task<List<Product>> GetAllProductsListAsync(string title, string description, int pageNumber = 1, int pageSize = 10); 
        Task<bool> IsUniqueProductAsync(string createdProductHash);
    }
}
