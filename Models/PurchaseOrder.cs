using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrderProducts = new HashSet<PurchaseOrderProduct>();
        }

        public int PurchaseOrderId { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual ICollection<PurchaseOrderProduct> PurchaseOrderProducts { get; set; }
    }
}
