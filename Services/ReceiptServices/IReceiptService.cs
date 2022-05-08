using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.ReceiptServices
{
    public interface IReceiptService
    {
        Task<bool> CreateReceipt(PurchaseOrder order);
    }
}
