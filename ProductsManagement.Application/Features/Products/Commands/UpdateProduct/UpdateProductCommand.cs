using MediatR;
using ProductsManagement.Application.Exceptions;
using ProductsManagement.Application.Interfaces.Repositories;
using ProductsManagement.Application.Wrappers;
using ProductsManagement.Domain.Enums;
using ProductsManagement.Infrastructure.Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Response<int>>
    {
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DietaryFlags DietaryFlags { get; set; }
        public int NumberOfViewsAndImpressions { get; set; }
        public string ImageUrl { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<int>>
        {
            private readonly Lazy<IProductRepositoryAsync> _productRepositoryAsync;
            public UpdateProductCommandHandler(Lazy<IProductRepositoryAsync> productRepositoryAsync)
            {
                _productRepositoryAsync = productRepositoryAsync;
            }

            private IProductRepositoryAsync ProductRepositoryAsync => _productRepositoryAsync.Value;

            public async Task<Response<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await ProductRepositoryAsync.GetByIdAsync(command.ProductId);
                product.Title = command.Title;
                product.Description = command.Description;
                product.DietaryFlags = command.DietaryFlags;
                product.Price = command.Price;
                product.NumberOfViewsAndImpressions = product.NumberOfViewsAndImpressions;
                product.VendorId = command.VendorId;
                product.ProductHash = await SecurityHelper.ComputeMD5Hash($"{command.Title} - {command.Description}");
                await ProductRepositoryAsync.UpdateAsync(product);
                return new Response<int>(product.Id);
            }
        }
    }
}
