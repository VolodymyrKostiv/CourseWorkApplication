using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CreateSupplyOrderViewModelFactory : IViewModelFactory<CreateSupplyOrderViewModel>
    {
        public CreateSupplyOrderViewModel CreateViewModel()
        {
            return new CreateSupplyOrderViewModel();
        }
    }
}
