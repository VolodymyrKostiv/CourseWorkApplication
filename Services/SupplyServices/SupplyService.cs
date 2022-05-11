using CourseWorkApplication.DTOs;
using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.SupplyServices
{
    public class SupplyService : ISupplyService
    {
        private IHttpAPIHelper<SupplyOrderDTO> _httpAPIHelperSupplyOrderDTO;
        private IHttpAPIHelper<SupplyOrder> _httpAPIHelperSupplyOrder;

        public SupplyService()
        {
            _httpAPIHelperSupplyOrderDTO = new HttpAPIHelper<SupplyOrderDTO>();
            _httpAPIHelperSupplyOrder = new HttpAPIHelper<SupplyOrder>();
        }

        public async Task<bool> CreateSupplyOrder(SupplyOrderDTO order)
        {
            if (order == null)
            {
                return false;
            }

            try
            {
                var result = await _httpAPIHelperSupplyOrderDTO.PostRequest("supplyOrders", order);
                return result != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
