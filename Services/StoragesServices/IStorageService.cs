using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.StoragesServices
{
    public interface IStorageService
    {
        public Task<IEnumerable<ShopStorageProduct>> LoadProductsFromStorage(int employeeId);
    }
}
