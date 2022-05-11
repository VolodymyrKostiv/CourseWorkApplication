using CourseWorkApplication.Commands;
using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel
{
    public class CheckSupplyOrdersViewModel : ViewModelBase
    {
        public IEnumerable<SupplyOrder> SupplyOrders { get; set; }
        private IHttpAPIHelper<SupplyOrder> _httpAPIHelper;
        private readonly IAuthenticator _authenticator;

        public RelayCommand SelectShowSupplyOrder { get; set; }

        private SupplyOrder _selectedSupplyOrder;
        public SupplyOrder SelectedSupplyOrder
        {
            get => _selectedSupplyOrder;
            set
            {
                _selectedSupplyOrder = value;
                OnPropertyChanged(nameof(SelectedSupplyOrder));
            }
        }

        public CheckSupplyOrdersViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            _httpAPIHelper = new HttpAPIHelper<SupplyOrder>();
            CreateCommands();
            LoadSupplyOrders();
        }

        private void CreateCommands()
        {
            SelectShowSupplyOrder = new RelayCommand(ShowSupplyOrder);
        }

        public void ShowSupplyOrder(object? parameter)
        {
            //
        }

        public async void LoadSupplyOrders()
        {
            try
            {
                SupplyOrders = await _httpAPIHelper.GetMultipleItemsRequest($"supplyOrders?employeeID={_authenticator.CurrentEmployee.EmployeeId}");
                OnPropertyChanged(nameof(SupplyOrders));
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
            OnPropertyChanged(nameof(SupplyOrders));
        }
    }
}
