using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.Services.ProductServices
{
    public interface IProductService
    {
        public Task<IEnumerable<FullProductInfo>> GetAllProductsInfo();
    }
}
