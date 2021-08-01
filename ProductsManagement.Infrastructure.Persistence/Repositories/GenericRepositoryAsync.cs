using Microsoft.EntityFrameworkCore;
using ProductsManagement.Application.Interfaces;
using ProductsManagement.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Infrastructure.Persistence.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GenericRepositoryAsync(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private IApplicationDbContext ApplicationDbContext => _applicationDbContext;

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await ApplicationDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await ApplicationDbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await ApplicationDbContext.Set<T>().AddAsync(entity);
            await ApplicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            ApplicationDbContext.Entry(entity).State = EntityState.Modified;
            await ApplicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            ApplicationDbContext.Set<T>().Remove(entity);
            await ApplicationDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await ApplicationDbContext
                 .Set<T>()
                 .ToListAsync();
        }
    }
}
