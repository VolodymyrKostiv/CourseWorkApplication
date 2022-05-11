using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel
{
    public class CheckReceiptsViewModel : ViewModelBase
    {
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; }

        private readonly IAuthenticator _authenticator;
        private IHttpAPIHelper<PurchaseOrder> httpAPIHelper;

        private IEnumerable<string> _purchaseProductsFromOrders;
        public IEnumerable<string> PurchasedProductsFromOrder 
        {
            get => _purchaseProductsFromOrders;
            set
            {
                _purchaseProductsFromOrders = value;
                OnPropertyChanged(nameof(PurchasedProductsFromOrder));
            }
        }

        public CheckReceiptsViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            httpAPIHelper = new HttpAPIHelper<PurchaseOrder>();
            PurchasedProductsFromOrder = new List<string>();
            UpdateBindings();
        }

        public async Task LoadPurchaseOrders()
        {
            try
            {
                PurchaseOrders = await httpAPIHelper.GetMultipleItemsRequest($"purchaseOrders?employeeID={_authenticator.CurrentEmployee.EmployeeId}");
                MergeProductsFromOrders();
                OnPropertyChanged(nameof(PurchaseOrders));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void MergeProductsFromOrders()
        {
            List<string> prods = new List<string>();
            foreach (PurchaseOrder purchaseOrder in PurchaseOrders)
            {
                string products = string.Empty;
                foreach (PurchaseOrderProduct purchaseProduct in purchaseOrder.PurchaseOrderProducts)
                {
                    string temp = "Name - " + purchaseProduct.Product.Title + ", Quantity - " + 
                        purchaseProduct.Quantity + ", Price per unit - " + purchaseProduct.Product.Price + "; ";
                    products += temp;
                }

                prods.Add(products);
            }
            _purchaseProductsFromOrders = prods;
        }

        public override void UpdateBindings()
        {
            base.UpdateBindings();
            LoadPurchaseOrders();
            OnPropertyChanged(nameof(PurchaseOrders));
            OnPropertyChanged(nameof(PurchasedProductsFromOrder));
        }
    }
}
