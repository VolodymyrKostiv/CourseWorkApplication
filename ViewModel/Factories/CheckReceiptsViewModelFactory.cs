using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CheckReceiptsViewModelFactory : IViewModelFactory<CheckReceiptsViewModel>
    {
        private readonly IAuthenticator _authenticator;

        public CheckReceiptsViewModelFactory(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public CheckReceiptsViewModel CreateViewModel()
        {
            return new CheckReceiptsViewModel(_authenticator);
        }
    }
}
