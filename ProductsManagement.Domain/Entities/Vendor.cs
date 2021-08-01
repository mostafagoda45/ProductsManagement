using ProductsManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductsManagement.Domain.Entities
{
    public class Vendor : AuditableBaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
