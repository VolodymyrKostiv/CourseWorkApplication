using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.DTOs
{
    public class PurchaseOrderProductDTO
    {
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
