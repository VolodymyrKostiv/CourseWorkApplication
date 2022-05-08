using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CreateReceiptViewModelFactory : IViewModelFactory<CreateReceiptViewModel>
    {
        public CreateReceiptViewModel CreateViewModel()
        {
            return new CreateReceiptViewModel();
        }
    }
}
