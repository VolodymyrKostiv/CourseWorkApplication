using CourseWorkApplication.State.Authentificators;
using CourseWorkApplication.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class LoginViewModelFactory : IViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator _authentificator;
        private readonly IRenavigator _renavigator;

        public LoginViewModelFactory(IAuthenticator authentificator, IRenavigator renavigator)
        {
            _authentificator = authentificator;
            _renavigator = renavigator;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(_authentificator, _renavigator);
        }
    }
}
