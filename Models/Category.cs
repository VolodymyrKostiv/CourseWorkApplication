using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class Category
    {
        public Category()
        {
            Subcategories = new HashSet<Subcategory>();
        }

        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
