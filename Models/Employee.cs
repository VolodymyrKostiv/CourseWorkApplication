using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class Employee
    {
        public Employee()
        {
            PurchaseOrders = new HashSet<PurchaseOrder>();
            SupplyOrders = new HashSet<SupplyOrder>();
        }

        public int EmployeeId { get; set; }
        public int ShopId { get; set; }
        public int EmployeeTypeId { get; set; }

        public virtual EmployeeType EmployeeType { get; set; } = null!;
        public virtual Shop Shop { get; set; } = null!;
        public virtual LoginDatum LoginDatum { get; set; } = null!;
        public virtual PersonalDatum PersonalDatum { get; set; } = null!;
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SupplyOrder> SupplyOrders { get; set; }
    }
}
