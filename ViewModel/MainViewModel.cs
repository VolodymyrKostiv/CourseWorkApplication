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
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }
        public IAuthenticator Authenticator { get; } 
        public ICommand UpdateCurrentViewModelCommand { get; set; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator, IViewModelAbstractFactory viewModelFactory)
        {
            Navigator = navigator;
            Authenticator = authenticator;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);

            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }
    }
}
