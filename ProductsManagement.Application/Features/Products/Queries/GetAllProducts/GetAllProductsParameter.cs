using ProductsManagement.Application.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsManagement.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsParameter : RequestParameter
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
