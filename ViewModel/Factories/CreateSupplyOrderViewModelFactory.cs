using CourseWorkApplication.Services.ProductServices;
using CourseWorkApplication.Services.SupplierServices;
using CourseWorkApplication.Services.SupplyServices;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CreateSupplyOrderViewModelFactory : IViewModelFactory<CreateSupplyOrderViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly ISupplyService _supplyService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;

        public CreateSupplyOrderViewModelFactory(IAuthenticator authenticator, 
            ISupplyService supplyService, IProductService productService,
            ISupplierService supplierService)
        {
            _authenticator = authenticator;
            _supplyService = supplyService;
            _productService = productService;
            _supplierService = supplierService;
        }

        public CreateSupplyOrderViewModel CreateViewModel()
        {
            return new CreateSupplyOrderViewModel(_authenticator, _supplyService, _productService, _supplierService);
        }
    }
}
