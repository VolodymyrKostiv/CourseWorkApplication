using CourseWorkApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWorkApplication.State.Navigators
{
    public enum ViewType
    {
        CreateReceipt,
        CheckReceipts,
        CreateSupplyOrder,
        CheckSupplyOrders,
        CheckStorage,
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
