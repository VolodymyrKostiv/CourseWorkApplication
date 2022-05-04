using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class Subcategory
    {
        public Subcategory()
        {
            Products = new HashSet<Product>();
        }

        public int SubcategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<Product> Products { get; set; }
    }
}
