using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace CourseWorkApplication.ViewModel
{
    public class CheckStorageViewModel : ViewModelBase
    {
        public IEnumerable<ShopStorageProduct> ShopStorageProducts { get; set; }

        private IHttpAPIHelper<ShopStorageProduct> httpAPIHelper;

        public CheckStorageViewModel()
        {
            httpAPIHelper = new HttpAPIHelper<ShopStorageProduct>();
            LoadStorageItems();
        }

        public async void LoadStorageItems()
        {
            try
            {
                ShopStorageProducts = await httpAPIHelper.GetMultipleItemsRequest("storages/employee?employeeID=1");
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
