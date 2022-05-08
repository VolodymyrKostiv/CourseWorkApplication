using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CheckStorageViewModelFactory : IViewModelFactory<CheckStorageViewModel>
    {
        public CheckStorageViewModel CreateViewModel()
        {
            return new CheckStorageViewModel();
        }
    }
}
