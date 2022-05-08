using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.AuthentificationServices
{
    public interface IAuthentificationService
    {
        Task<Employee> LogIn(string username, string password);
    }
}
