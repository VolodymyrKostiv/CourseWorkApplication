using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CourseWorkApplication.ViewModel
{
    public class CheckStorageViewModel : ViewModelBase
    {
        public IEnumerable<ShopStorageProduct> ShopStorageProducts { get; set; }

        private readonly IAuthenticator _authenticator;
        private IHttpAPIHelper<ShopStorageProduct> httpAPIHelper;

        public CheckStorageViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            httpAPIHelper = new HttpAPIHelper<ShopStorageProduct>();
            UpdateBindings();
        }

        public async Task LoadStorageItems()
        {
            try
            {
                ShopStorageProducts = await httpAPIHelper.GetMultipleItemsRequest($"storages/employee?employeeID={_authenticator.CurrentEmployee.EmployeeId}");
                OnPropertyChanged(nameof(ShopStorageProducts));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void UpdateBindings()
        {
            base.UpdateBindings();
            LoadStorageItems();
        }
    }
}
