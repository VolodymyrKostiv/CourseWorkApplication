using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class PurchaseOrderProduct
    {
        public int PurchaseOrderProductId { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int PurchaseOrderId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual PurchaseOrder PurchaseOrder { get; set; } = null!;
    }
}
