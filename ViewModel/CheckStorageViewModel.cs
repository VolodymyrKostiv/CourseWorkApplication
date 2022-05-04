using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using System;
using System.Collections.Generic;

namespace CourseWorkApplication.ViewModel
{
    public class CheckStorageViewModel : ViewModelBase
    {
        public IEnumerable<Product> Products { get; set; }

        private IHttpAPIHelper<Product> httpAPIHelper;

        public CheckStorageViewModel()
        {
            httpAPIHelper = new HttpAPIHelper<Product>();
            LoadPurchaseOrders();
        }

        public async void LoadPurchaseOrders()
        {
            try
            {
                //Products = await httpAPIHelper.GetMultipleItemsRequest("products");
                Products = await httpAPIHelper.GetMultipleItemsRequest("storages/employee/1/");
                OnPropertyChanged(nameof(Products));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
