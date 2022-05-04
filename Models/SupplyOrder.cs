using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class SupplyOrder
    {
        public SupplyOrder()
        {
            SupplyOrderProducts = new HashSet<SupplyOrderProduct>();
        }

        public int SupplyOrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public int SupplierId { get; set; }
        public int EmployeeId { get; set; }
        public int SupplyOrderStateId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Supplier Supplier { get; set; } = null!;
        public virtual SupplyOrderState SupplyOrderState { get; set; } = null!;
        public virtual ICollection<SupplyOrderProduct> SupplyOrderProducts { get; set; }
    }
}
