using CourseWorkApplication.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class ViewModelAbstractFactory : IViewModelAbstractFactory
    {
        private readonly IViewModelFactory<CheckReceiptsViewModel> _checkReceiptsViewModelFactory;
        private readonly IViewModelFactory<CheckStorageViewModel> _checkStorageViewModelFactory;
        private readonly IViewModelFactory<CheckSupplyOrdersViewModel> _checkSupplyOrdersViewModelFactory;
        private readonly IViewModelFactory<CreateReceiptViewModel> _createReceiptViewModelFactory;
        private readonly IViewModelFactory<CreateSupplyOrderViewModel> _createSupplyOrderViewModelFactory;
        private readonly IViewModelFactory<LoginViewModel> _loginViewModelViewModelFactory;

        public ViewModelAbstractFactory(
            IViewModelFactory<CheckReceiptsViewModel> checkReceiptsViewModelFactory,
            IViewModelFactory<CheckStorageViewModel> checkStorageViewModelFactory,
            IViewModelFactory<CheckSupplyOrdersViewModel> checkSupplyOrdersViewModelFactory,
            IViewModelFactory<CreateReceiptViewModel> createReceiptViewModelFactory,
            IViewModelFactory<CreateSupplyOrderViewModel> createSupplyOrderViewModelFactory,
            IViewModelFactory<LoginViewModel> loginViewModelViewModelFactory)
        {
            _checkReceiptsViewModelFactory = checkReceiptsViewModelFactory;
            _checkStorageViewModelFactory = checkStorageViewModelFactory;
            _checkSupplyOrdersViewModelFactory = checkSupplyOrdersViewModelFactory;
            _createReceiptViewModelFactory = createReceiptViewModelFactory;
            _createSupplyOrderViewModelFactory = createSupplyOrderViewModelFactory;
            _loginViewModelViewModelFactory = loginViewModelViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.CheckReceipts:
                    return _checkReceiptsViewModelFactory.CreateViewModel();
                case ViewType.CheckStorage:
                    return _checkStorageViewModelFactory.CreateViewModel();
                case ViewType.CheckSupplyOrders:
                    return _checkSupplyOrdersViewModelFactory.CreateViewModel();
                case ViewType.CreateReceipt:
                    return _createReceiptViewModelFactory.CreateViewModel();
                case ViewType.CreateSupplyOrder:
                    return _createSupplyOrderViewModelFactory.CreateViewModel();
                case ViewType.Login:
                    return _loginViewModelViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("Invalid ViewModelType");
            }
        }
    }
}
