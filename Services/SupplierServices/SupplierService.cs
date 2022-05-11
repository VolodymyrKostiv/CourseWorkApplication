using CourseWorkApplication.DTOs;
using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.SupplierServices
{
    public class SupplierService : ISupplierService
    {
        private IHttpAPIHelper<SupplierDTO> _httpAPIHelper;

        public SupplierService()
        {
            _httpAPIHelper = new HttpAPIHelper<SupplierDTO>(); 
        }

        public async Task<IEnumerable<SupplierDTO>> GetAllSuppliers()
        {
            try
            {
                IEnumerable<SupplierDTO> res = await _httpAPIHelper.GetMultipleItemsRequest("suppliers");
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
