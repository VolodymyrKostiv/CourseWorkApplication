using CourseWorkApplication.Commands;
using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWorkApplication.ViewModel
{
    public class CheckReceiptsViewModel : ViewModelBase
    {
        public ObservableCollection<PurchaseOrder> PurchaseOrders { get; set; }
        private readonly IAuthenticator _authenticator;
        private IHttpAPIHelper<PurchaseOrder> httpAPIHelper;

        public RelayCommand SelectShowProductsFromOrder { get; set; }
        public  RelayCommand SelectDeletePurchaseOrder { get; set; }

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
            SelectDeletePurchaseOrder = new RelayCommand(DeletePurchaseOrder);
        }

        private async void DeletePurchaseOrder(object obj)
        {
            try
            {
                var order = obj as PurchaseOrder;

                if (order != null && MessageBox.Show("Do you really want to delete selected order", "Delete order", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    if (await httpAPIHelper.DeleteRequest($"purchaseOrders?orderID={order.PurchaseOrderId}") != null)
                    {
                        MessageBox.Show("Order successfully deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Error during deleting order", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    UpdateBindings();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ShowProductsFromOrder(object? parameter)
        {
            //
        }

        private async Task LoadPurchaseOrders()
        {
            try
            {
                var x = await httpAPIHelper.GetMultipleItemsRequest($"purchaseOrders?employeeID={_authenticator.CurrentEmployee.EmployeeId}");
                PurchaseOrders = new ObservableCollection<PurchaseOrder>(x);
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
