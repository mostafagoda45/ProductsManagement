using ProductsManagement.Application.Interfaces;
using ProductsManagement.Application.Interfaces.Repositories;
using ProductsManagement.Domain.Entities;
using ProductsManagement.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsManagement.Infrastructure.Persistence.Repositories
{
    public class VendorRepositoryAsync : GenericRepositoryAsync<Vendor>, IVendorRepositoryAsync
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public VendorRepositoryAsync(IApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private IApplicationDbContext ApplicationDbContext => _applicationDbContext;
    }
}
