using MediatR;
using ProductsManagement.Application.Exceptions;
using ProductsManagement.Application.Interfaces.Repositories;
using ProductsManagement.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Features.Products.Commands.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Response<int>>
        {
            private readonly Lazy<IProductRepositoryAsync> _productRepositoryAsync;
            public DeleteProductByIdCommandHandler(Lazy<IProductRepositoryAsync> productRepositoryAsync)
            {
                _productRepositoryAsync = productRepositoryAsync;
            }

            private IProductRepositoryAsync ProductRepositoryAsync => _productRepositoryAsync.Value;

            public async Task<Response<int>> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await ProductRepositoryAsync.GetByIdAsync(command.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                await ProductRepositoryAsync.DeleteAsync(product);
                return new Response<int>(product.Id);
            }
        }
    }
}
