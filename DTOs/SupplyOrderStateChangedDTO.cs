using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.DTOs
{
    public class SupplyOrderStateChangedDTO
    {
        public int SupplyOrderId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? ActualDate { get; set; }
        public int SupplyOrderStateId { get; set; }
    }
}
