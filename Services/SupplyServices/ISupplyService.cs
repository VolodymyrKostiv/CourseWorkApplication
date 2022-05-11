using CourseWorkApplication.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.SupplyServices
{
    public interface ISupplyService
    {
        Task<bool> CreateSupplyOrder(SupplyOrderDTO order);
    }
}
