using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.DTOs
{
    public class SupplyOrderDTO
    {
        public SupplyOrderDTO()
        {
            SupplyOrderProducts = new HashSet<SupplyOrderProductDTO>();
        }

        public int SupplyOrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public int SupplierId { get; set; }
        public int EmployeeId { get; set; }
        public int SupplyOrderStateId { get; set; }

        public SupplierDTO Supplier { get; set; }
        public SupplyOrderStateDTO SupplyOrderState { get; set; } 
        public ICollection<SupplyOrderProductDTO> SupplyOrderProducts { get; set; }
    }
}
