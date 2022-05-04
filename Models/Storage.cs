using System;
using System.Collections.Generic;

namespace CourseWorkApplication.Models
{
    public partial class Storage
    {
        public Storage()
        {
            Shops = new HashSet<Shop>();
        }

        public int StorageId { get; set; }
        public double? Area { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }
    }
}
