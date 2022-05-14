using CourseWorkApplication.Enums;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.State.Authentificators
{
    public interface IAuthenticator
    {
        Employee CurrentEmployee { get; }
        UserType EmployeeUserType { get; }

        bool IsLoggedIn { get; }
        bool IsSeller { get; }
        bool IsManager { get; }
        Task<bool> LogIn(string username, string password);
        void LogOut();
    }
}
