using MediatR;
using ProductsManagement.Application.Exceptions;
using ProductsManagement.Application.Interfaces.Repositories;
using ProductsManagement.Application.Wrappers;
using ProductsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<Response<Product>>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<Product>>
        {
            private readonly Lazy<IProductRepositoryAsync> _productRepositoryAsync;
            public GetProductByIdQueryHandler(Lazy<IProductRepositoryAsync> productRepositoryAsync)
            {
                _productRepositoryAsync = productRepositoryAsync;
            }

            private IProductRepositoryAsync ProductRepositoryAsync => _productRepositoryAsync.Value;

            public async Task<Response<Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await ProductRepositoryAsync.GetByIdAsync(query.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                return new Response<Product>(product);
            }
        }
    }
}
