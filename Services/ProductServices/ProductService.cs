using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private IHttpAPIHelper<FullProductInfo> _httpAPIHelper;

        public ProductService()
        {
            _httpAPIHelper = new HttpAPIHelper<FullProductInfo>();
        }

        public async Task<IEnumerable<FullProductInfo>> GetAllProductsInfo()
        {
            try
            {
                IEnumerable<FullProductInfo> res = await _httpAPIHelper.GetMultipleItemsRequest("products");
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
