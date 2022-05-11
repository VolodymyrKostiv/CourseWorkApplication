using CourseWorkApplication.Commands;
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

        public RelayCommand SelectShowProductsFromOrder { get; set; }

        private PurchaseOrder _selectedPurchaseOrder;
        public PurchaseOrder SelectedPurchaseOrder
        {
            get => _selectedPurchaseOrder;
            set
            {
                _selectedPurchaseOrder = value;
                OnPropertyChanged(nameof(SelectedPurchaseOrder));
            }
        }

        public CheckReceiptsViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            httpAPIHelper = new HttpAPIHelper<PurchaseOrder>();
            UpdateBindings();
            CreateCommands();
        }

        private void CreateCommands()
        {
            SelectShowProductsFromOrder = new RelayCommand(ShowProductsFromOrder);
        }

        public void ShowProductsFromOrder(object? parameter)
        {
            //
        }

        public async Task LoadPurchaseOrders()
        {
            try
            {
                PurchaseOrders = await httpAPIHelper.GetMultipleItemsRequest($"purchaseOrders?employeeID={_authenticator.CurrentEmployee.EmployeeId}");
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
            LoadPurchaseOrders();
            OnPropertyChanged(nameof(PurchaseOrders));
        }
    }
}
