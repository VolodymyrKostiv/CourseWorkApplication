using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.AuthentificationServices
{
    public class AuthentificationService : IAuthentificationService
    {
        private IHttpAPIHelper<Employee> httpAPIHelper;

        public AuthentificationService()
        {
            httpAPIHelper = new HttpAPIHelper<Employee>();
        }

        public async Task<Employee> LogIn(string username, string password)
        {
            if (username == null || username == string.Empty || password == null || password == string.Empty)
            {
                return null;
            }

            try
            {
                var result = await httpAPIHelper.GetSingleItemRequest($"authentification?username={username}&password={password}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
