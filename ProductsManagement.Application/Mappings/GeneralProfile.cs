using AutoMapper;
using ProductsManagement.Application.Features.Products.Commands.CreateProduct;
using ProductsManagement.Application.Features.Products.Queries.GetAllProducts;
using ProductsManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsManagement.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
