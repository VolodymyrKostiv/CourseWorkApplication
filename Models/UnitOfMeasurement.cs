using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class UnitOfMeasurement
    {
        public UnitOfMeasurement()
        {
            Products = new HashSet<Product>();
        }

        public int UnitOfMeasurementId { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
