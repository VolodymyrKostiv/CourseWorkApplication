using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CourseWorkApplication.ViewModel
{
    public class CheckStorageViewModel : ViewModelBase
    {
        public ObservableCollection<ShopStorageProduct> ShopStorageProducts { get; set; }
        private readonly IAuthenticator _authenticator;
        private IHttpAPIHelper<ShopStorageProduct> _httpAPIHelper;

        public CheckStorageViewModel(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            _httpAPIHelper = new HttpAPIHelper<ShopStorageProduct>();
            UpdateBindings();
        }

        public async Task LoadStorageItems()
        {
            try
            {
                var x = await _httpAPIHelper.GetMultipleItemsRequest($"storages/employee?employeeID={_authenticator.CurrentEmployee.EmployeeId}");
                ShopStorageProducts = new ObservableCollection<ShopStorageProduct>(x);
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
