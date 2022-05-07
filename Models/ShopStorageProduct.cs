using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Models
{
    public class ShopStorageProduct
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
    }
}
