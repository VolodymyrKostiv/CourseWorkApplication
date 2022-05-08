using CourseWorkApplication.Commands;
using CourseWorkApplication.State.Authentificators;
using CourseWorkApplication.State.Navigators;
using CourseWorkApplication.ViewModel.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseWorkApplication.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        public ICommand LoginCommand { get; set; }

        public LoginViewModel(IAuthenticator authentificator, IRenavigator renavigator)
        {
            LoginCommand = new LoginCommand(this, authentificator, renavigator);
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value; 
                OnPropertyChanged(nameof(Username));
            }
        }

        public override void UpdateBindings()
        {
            base.UpdateBindings();
            OnPropertyChanged(nameof(Username));
        }
    }
}
