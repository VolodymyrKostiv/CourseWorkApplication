using CourseWorkApplication.State.Navigators;
using CourseWorkApplication.ViewModel;
using System;
using System.Windows.Input;

namespace CourseWorkApplication.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (CanExecute(parameter) && parameter is ViewType viewType)
            {
                switch (viewType)
                {
                    case ViewType.CreateReceipt:
                        _navigator.CurrentViewModel = new CreateReceiptViewModel();
                        break;
                    case ViewType.CheckReceipts:
                        _navigator.CurrentViewModel = new CheckReceiptsViewModel();
                        break;
                    case ViewType.CreateSupplyOrder:
                        _navigator.CurrentViewModel = new CreateSupplyOrderViewModel();
                        break;
                    case ViewType.CheckSupplyOrders:
                        _navigator.CurrentViewModel = new CheckSupplyOrdersViewModel();
                        break;
                    case ViewType.CheckStorage:
                        _navigator.CurrentViewModel = new CheckStorageViewModel();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
