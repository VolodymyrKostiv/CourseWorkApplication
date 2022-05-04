using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class Product
    {
        public Product()
        {
            PurchaseOrderProducts = new HashSet<PurchaseOrderProduct>();
            SupplyOrderProducts = new HashSet<SupplyOrderProduct>();
        }

        public int ProductId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? BrandId { get; set; }
        public int? UnitOfMeasurementId { get; set; }
        public int? SubcategoryId { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Subcategory? Subcategory { get; set; }
        public virtual UnitOfMeasurement? UnitOfMeasurement { get; set; }
        public virtual ICollection<PurchaseOrderProduct> PurchaseOrderProducts { get; set; }
        public virtual ICollection<SupplyOrderProduct> SupplyOrderProducts { get; set; }
    }
}
