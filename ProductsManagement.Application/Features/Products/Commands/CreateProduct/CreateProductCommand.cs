using AutoMapper;
using MediatR;
using ProductsManagement.Application.Interfaces.Repositories;
using ProductsManagement.Application.Wrappers;
using ProductsManagement.Domain.Entities;
using ProductsManagement.Domain.Enums;
using ProductsManagement.Infrastructure.Persistence.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Features.Products.Commands.CreateProduct
{
    public partial class CreateProductCommand : IRequest<Response<int>>
    {
        public int VendorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DietaryFlags DietaryFlags { get; set; }
        public int NumberOfViewsAndImpressions { get; set; }
        public string ImageUrl { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<int>>
    {
        private readonly Lazy<IProductRepositoryAsync> _productRepositoryAsync;
        private readonly Lazy<IMapper> _mapper;
        public CreateProductCommandHandler(Lazy<IProductRepositoryAsync> productRepositoryAsync, Lazy<IMapper> mapper)
        {
            _productRepositoryAsync = productRepositoryAsync;
            _mapper = mapper;
        }

        private IProductRepositoryAsync ProductRepositoryAsync => _productRepositoryAsync.Value;
        private IMapper Mapper => _mapper.Value;

        public async Task<Response<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Mapper.Map<Product>(request);
            product.ProductHash = await SecurityHelper.ComputeMD5Hash($"{product.Title} - {product.Description}");
            await ProductRepositoryAsync.AddAsync(product);
            return new Response<int>(product.Id);
        }
    }
}
