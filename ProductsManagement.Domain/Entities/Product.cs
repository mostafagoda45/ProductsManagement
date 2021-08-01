using ProductsManagement.Domain.Common;
using ProductsManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductsManagement.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DietaryFlags DietaryFlags { get; set; }
        public int NumberOfViewsAndImpressions { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string ProductHash { get; set; }
    }
}
