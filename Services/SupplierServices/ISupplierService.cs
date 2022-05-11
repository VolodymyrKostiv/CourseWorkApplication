using CourseWorkApplication.DTOs;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.SupplierServices
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetAllSuppliers(); 
    }
}
