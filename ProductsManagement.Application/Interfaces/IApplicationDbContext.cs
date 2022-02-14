using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProductsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Interfaces
{
    public interface IApplicationDbContext : IDisposable
    { 
        DatabaseFacade Database { get; }
        IDbConnection GetDbContextConnection();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
        DbSet<T> Set<T>() where T : class;
        DbSet<Product> Products { get; set; }
        DbSet<Vendor> Vendors { get; set; }
    }
}
