using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Employees = new HashSet<Employee>();
        }

        public int ShopId { get; set; }
        public string Address { get; set; } = null!;
        public int StorageId { get; set; }

        public virtual Storage Storage { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
