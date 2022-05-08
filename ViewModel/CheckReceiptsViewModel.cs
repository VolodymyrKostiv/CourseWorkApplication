using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;

namespace CourseWorkApplication.ViewModel
{
    public class CheckReceiptsViewModel : ViewModelBase
    {
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; }
        
        private IHttpAPIHelper<PurchaseOrder> httpAPIHelper;

        public CheckReceiptsViewModel()
        {
            httpAPIHelper = new HttpAPIHelper<PurchaseOrder>();
            LoadPurchaseOrders();
        }

        public async void LoadPurchaseOrders()
        {
            try
            {
                PurchaseOrders = await httpAPIHelper.GetMultipleItemsRequest("purchaseOrders?employeeID=1");
                OnPropertyChanged(nameof(PurchaseOrders));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void UpdateBindings()
        {
            base.UpdateBindings();
        }
    }
}
