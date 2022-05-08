using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CheckReceiptsViewModelFactory : IViewModelFactory<CheckReceiptsViewModel>
    {
        public CheckReceiptsViewModel CreateViewModel()
        {
            return new CheckReceiptsViewModel();
        }
    }
}
