using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel
{
    public class CheckSupplyOrdersViewModel : ViewModelBase
    {
        public IEnumerable<SupplyOrder> ShopSupplyOrders { get; set; }

        private IHttpAPIHelper<SupplyOrder> httpAPIHelper;

        public CheckSupplyOrdersViewModel()
        {
            httpAPIHelper = new HttpAPIHelper<SupplyOrder>();
            LoadSupplyOrders();
        }

        public async void LoadSupplyOrders()
        {
            try
            {
                ShopSupplyOrders = await httpAPIHelper.GetMultipleItemsRequest("supplyOrders?employeeID=1");
                OnPropertyChanged(nameof(ShopSupplyOrders));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void UpdateBindings()
        {
            base.UpdateBindings();
            LoadSupplyOrders();
        }
    }
}
