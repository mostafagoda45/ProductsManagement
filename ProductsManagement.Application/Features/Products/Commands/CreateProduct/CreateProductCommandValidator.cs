using FluentValidation;
using Microsoft.EntityFrameworkCore.Internal;
using ProductsManagement.Application.Interfaces.Repositories;
using ProductsManagement.Domain.CustomEntites;
using ProductsManagement.Domain.Entities;
using ProductsManagement.Infrastructure.Persistence.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductsManagement.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly Lazy<IProductRepositoryAsync> _productRepository;
        private readonly Lazy<IVendorRepositoryAsync> _vendorRepository;

        public CreateProductCommandValidator(Lazy<IProductRepositoryAsync> productRepository, Lazy<IVendorRepositoryAsync> vendorRepository)
        {
            _productRepository = productRepository;
            _vendorRepository = vendorRepository;

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("Title is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("Title must not exceed 50 characters.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("Price is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(p => p.VendorId)
                .NotEmpty().WithMessage("VendorId is required")
                .NotNull()
                .MustAsync(IsVendorExists).WithMessage("Vendor dosen't exist.");

            RuleFor(p => new ProductAssociatedHashFields { Title = p.Title, Description = p.Description })
                .MustAsync(IsUniqueProduct).WithMessage("Product already exists");
        }

        private IProductRepositoryAsync ProductRepository => _productRepository.Value;
        private IVendorRepositoryAsync VendorRepository => _vendorRepository.Value;

        private async Task<bool> IsVendorExists(int vendorId, CancellationToken cancellationToken)
        {
            var vendor = await VendorRepository.GetByIdAsync(vendorId);
            return vendor != null;
        }

        private async Task<bool> IsUniqueProduct(ProductAssociatedHashFields productAssociatedHashFields, CancellationToken cancellationToken)
        {
            return await ProductRepository.IsUniqueProductAsync(await SecurityHelper.ComputeMD5Hash($"{productAssociatedHashFields.Title} - {productAssociatedHashFields.Description}"));
        }
    }
}
