using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.Services.StoragesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.StoragesServices
{
    internal class StorageService : IStorageService
    {
        private IHttpAPIHelper<ShopStorageProduct> _httpAPIHelper;

        public StorageService()
        {
            _httpAPIHelper = new HttpAPIHelper<ShopStorageProduct>();
        }

        public async Task<IEnumerable<ShopStorageProduct>> LoadProductsFromStorage(int employeeId)
        {
            try
            {
                IEnumerable<ShopStorageProduct> res = await _httpAPIHelper.GetMultipleItemsRequest($"storages/employee?employeeID={employeeId}");
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
