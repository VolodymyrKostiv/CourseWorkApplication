using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            SupplyOrders = new HashSet<SupplyOrder>();
        }

        public int SupplierId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<SupplyOrder> SupplyOrders { get; set; }
    }
}
