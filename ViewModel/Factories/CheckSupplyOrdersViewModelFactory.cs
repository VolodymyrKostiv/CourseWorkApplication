using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CheckSupplyOrdersViewModelFactory : IViewModelFactory<CheckSupplyOrdersViewModel>
    {
        public CheckSupplyOrdersViewModel CreateViewModel()
        {
            return new CheckSupplyOrdersViewModel();
        }
    }
}
