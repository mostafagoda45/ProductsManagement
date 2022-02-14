using ProductsManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsManagement.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsViewModel
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DietaryFlags DietaryFlags { get; set; }
        public int NumberOfViewsAndImpressions { get; set; }
        public string ImageUrl { get; set; }
    }
}
