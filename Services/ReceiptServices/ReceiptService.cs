using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.ReceiptServices
{
    public class ReceiptService : IReceiptService
    {
        private IHttpAPIHelper<PurchaseOrder> httpAPIHelper;

        public ReceiptService()
        {
            httpAPIHelper = new HttpAPIHelper<PurchaseOrder>();
        }

        public async Task<bool> CreateReceipt(PurchaseOrder order)
        {
            if (order == null)
            {
                return false;
            }

            try
            {
                var result = await httpAPIHelper.PostRequest("purchaseOrders/", order);
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
