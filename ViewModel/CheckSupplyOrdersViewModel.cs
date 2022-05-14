using CourseWorkApplication.Commands;
using CourseWorkApplication.DTOs;
using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWorkApplication.ViewModel
{
    public class CheckSupplyOrdersViewModel : ViewModelBase
    {
        public ObservableCollection<SupplyOrder> SupplyOrders { get; set; }
        public ObservableCollection<SupplyOrderState> SupplyOrdersState { get; set; }
        private IHttpAPIHelper<SupplyOrder> _httpAPIHelper;
        private IHttpAPIHelper<SupplyOrderState> _httpAPIHelperSupplyStates;
        private IHttpAPIHelper<SupplyOrderStateChangedDTO> _httpAPIHelperChangedOrderState;
        private readonly IAuthenticator _authenticator;

        public RelayCommand SelectShowSupplyOrder { get; set; }
        public RelayCommand SelectDeleteSupplyOrder { get; set; }

        public RelayCommand SelectStateReview { get; set; }
        public RelayCommand SelectStateInProgress { get; set; }
        public RelayCommand SelectStateCompleted { get; set; }
        public RelayCommand SelectStateCancelled { get; set; }


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
            _httpAPIHelperSupplyStates = new HttpAPIHelper<SupplyOrderState>();
            _httpAPIHelperChangedOrderState = new HttpAPIHelper<SupplyOrderStateChangedDTO>();
            CreateCommands();
            LoadStates();
            LoadSupplyOrders();
        }

        private void CreateCommands()
        {
            SelectShowSupplyOrder = new RelayCommand(ShowSupplyOrder);
            SelectDeleteSupplyOrder = new RelayCommand(DeleteSupplyOrder);

            SelectStateReview = new RelayCommand(SetStatusReview);
            SelectStateInProgress = new RelayCommand(SetStatusInProgress);
            SelectStateCompleted = new RelayCommand(SetStatusCompleted);
            SelectStateCancelled = new RelayCommand(SetStatusCancelled);
        }

        public void SetStatusReview(object? parameter)
        {
            var order = parameter as SupplyOrder;
            order.SupplyOrderStateId = SupplyOrdersState.Where(x => x.Title == "Review").Select(x => x.SupplyOrderStateId).First();
            order.SupplyOrderState = SupplyOrdersState.Where(x => x.Title == "Review").First();

            var orderDTO = new SupplyOrderStateChangedDTO() 
            { 
                SupplyOrderId = order.SupplyOrderId, 
                SupplyOrderStateId = order.SupplyOrderStateId 
            };

            _httpAPIHelperChangedOrderState.PutRequest("supplyOrders", orderDTO);
        }

        public void SetStatusInProgress(object? parameter)
        {
            var order = parameter as SupplyOrder;
            order.SupplyOrderStateId = SupplyOrdersState.Where(x => x.Title == "In progress").Select(x => x.SupplyOrderStateId).First();
            order.SupplyOrderState = SupplyOrdersState.Where(x => x.Title == "In progress").First();

            var orderDTO = new SupplyOrderStateChangedDTO()
            {
                SupplyOrderId = order.SupplyOrderId,
                SupplyOrderStateId = order.SupplyOrderStateId,
            };

            _httpAPIHelperChangedOrderState.PutRequest("supplyOrders", orderDTO);
        }

        public void SetStatusCompleted(object? parameter)
        {
            var order = parameter as SupplyOrder;
            order.SupplyOrderStateId = SupplyOrdersState.Where(x => x.Title == "Completed").Select(x => x.SupplyOrderStateId).First();
            order.SupplyOrderState = SupplyOrdersState.Where(x => x.Title == "Completed").First();

            var orderDTO = new SupplyOrderStateChangedDTO()
            {
                SupplyOrderId = order.SupplyOrderId,
                SupplyOrderStateId = order.SupplyOrderStateId,
                ActualDate = DateTime.UtcNow,
            };

            _httpAPIHelperChangedOrderState.PutRequest("supplyOrders", orderDTO);
        }

        public void SetStatusCancelled(object? parameter)
        {
            var order = parameter as SupplyOrder;
            order.SupplyOrderStateId = SupplyOrdersState.Where(x => x.Title == "Canceled").Select(x => x.SupplyOrderStateId).First();
            order.SupplyOrderState = SupplyOrdersState.Where(x => x.Title == "Canceled").First();

            var orderDTO = new SupplyOrderStateChangedDTO()
            {
                SupplyOrderId = order.SupplyOrderId,
                SupplyOrderStateId = order.SupplyOrderStateId,
                ActualDate = DateTime.UtcNow,
            };

            _httpAPIHelperChangedOrderState.PutRequest("supplyOrders", orderDTO);
        }


        public void ShowSupplyOrder(object? parameter)
        {
            //
        }

        public async void DeleteSupplyOrder(object? parameter)
        {
            var order = parameter as SupplyOrder;
            if (MessageBox.Show("Do you really want to delete selected order", "?", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                if (await _httpAPIHelper.DeleteRequest($"supplyOrders?supplyOrderId={order.SupplyOrderId}") != null)
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

        public async Task LoadSupplyOrders()
        {
            try
            {
                var x = await _httpAPIHelper.GetMultipleItemsRequest($"supplyOrders?employeeID={_authenticator.CurrentEmployee.EmployeeId}");
                SupplyOrders = new ObservableCollection<SupplyOrder>(x);
                OnPropertyChanged(nameof(SupplyOrders));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        private async void LoadStates()
        {
            try
            {
                var x = await _httpAPIHelperSupplyStates.GetMultipleItemsRequest("supplyOrderStates");
                SupplyOrdersState = new ObservableCollection<SupplyOrderState>(x);
                OnPropertyChanged(nameof(SupplyOrdersState));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void UpdateBindings()
        {
            base.UpdateBindings();
            LoadStates();
            LoadSupplyOrders();
            OnPropertyChanged(nameof(SupplyOrders));
        }
    }
}
