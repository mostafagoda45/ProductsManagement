using AutoMapper;
using MediatR;
using ProductsManagement.Application.Filters;
using ProductsManagement.Application.Interfaces.Repositories;
using ProductsManagement.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<PagedResponse<IEnumerable<GetAllProductsViewModel>>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<IEnumerable<GetAllProductsViewModel>>>
    {
        private readonly Lazy<IProductRepositoryAsync> _productRepositoryAsync;
        private readonly Lazy<IMapper> _mapper;
        public GetAllProductsQueryHandler(Lazy<IProductRepositoryAsync> productRepositoryAsync, Lazy<IMapper> mapper)
        {
            _productRepositoryAsync = productRepositoryAsync;
            _mapper = mapper;
        }

        private IProductRepositoryAsync ProductRepositoryAsync => _productRepositoryAsync.Value;
        private IMapper Mapper => _mapper.Value;

        public async Task<PagedResponse<IEnumerable<GetAllProductsViewModel>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = Mapper.Map<GetAllProductsParameter>(request);
            var product = await ProductRepositoryAsync.GetAllProductsListAsync(validFilter.Title, validFilter.Description, validFilter.PageNumber, validFilter.PageSize);
            var productViewModel = Mapper.Map<IEnumerable<GetAllProductsViewModel>>(product);
            return new PagedResponse<IEnumerable<GetAllProductsViewModel>>(productViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
