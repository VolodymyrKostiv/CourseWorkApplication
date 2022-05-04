using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class SupplyOrderProduct
    {
        public int SupplyOrderProductId { get; set; }
        public double Quantity { get; set; }
        public int ProductId { get; set; }
        public int SupplyOrderId { get; set; }

        public virtual Product Product { get; set; } = null!;
        public virtual SupplyOrder SupplyOrder { get; set; } = null!;
    }
}
