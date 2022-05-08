using CourseWorkApplication.Enums;
using CourseWorkApplication.Models;
using CourseWorkApplication.Services.AuthentificationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.State.Authentificators
{
    public class Authenticator : ObservableObject, IAuthenticator
    {
        private Employee _currentEmployee;
        public Employee CurrentEmployee 
        { 
            get => _currentEmployee;
            private set
            {
                _currentEmployee = value;
                OnPropertyChanged(nameof(CurrentEmployee));
                OnPropertyChanged(nameof(EmployeeUserType));
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(IsSeller));
                OnPropertyChanged(nameof(IsManager));
            }
        }

        private UserType _employeeUserType;
        public UserType EmployeeUserType
        {
            get => _employeeUserType;
            set
            {
                _employeeUserType = value;
                OnPropertyChanged(nameof(EmployeeUserType));
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(IsSeller));
                OnPropertyChanged(nameof(IsManager));
            }
        }

        public bool IsSeller => EmployeeUserType == UserType.Seller;
        public bool IsManager => EmployeeUserType == UserType.Manager;
        public bool IsLoggedIn => CurrentEmployee != null;
        

        private readonly IAuthentificationService _authentificationService;

        public Authenticator(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }

        public async Task<bool> LogIn(string username, string password)
        {
            try
            {
                var user = await _authentificationService.LogIn(username, password);
                if (user == null)
                {
                    return false;
                }

                CurrentEmployee = user;
                EmployeeUserType = (UserType)CurrentEmployee.EmployeeType.EmployeeTypeId;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public void LogOut()
        {
            CurrentEmployee = null;
        }
    }
}
