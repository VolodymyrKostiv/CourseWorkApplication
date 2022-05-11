using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CheckSupplyOrdersViewModelFactory : IViewModelFactory<CheckSupplyOrdersViewModel>
    {
        private readonly IAuthenticator _authenticator;

        public CheckSupplyOrdersViewModelFactory(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public CheckSupplyOrdersViewModel CreateViewModel()
        {
            return new CheckSupplyOrdersViewModel(_authenticator);
        }
    }
}
