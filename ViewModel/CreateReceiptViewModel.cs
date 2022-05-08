﻿using CourseWorkApplication.Commands;
using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.Services.ReceiptServices;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CourseWorkApplication.ViewModel
{
    public class CreateReceiptViewModel : ViewModelBase
    {
        public ObservableCollection<ShopStorageProduct> ShopStorageProducts { get; set; }

        private ObservableCollection<PurchaseOrderProduct> _purchaseOrderProducts;
        public ObservableCollection<PurchaseOrderProduct> PurchaseOrderProducts 
        {
            get => _purchaseOrderProducts;
            set
            {
                _purchaseOrderProducts = value;
                OnPropertyChanged(nameof(PurchaseOrderProducts));
            }
        }
        
        private IHttpAPIHelper<ShopStorageProduct> httpAPIHelper;
        private readonly IReceiptService _receiptService;

        public DateTime CurrentDate => DateTime.UtcNow;
        public string ShopAddress => CurrentEmployee?.Shop?.Address ?? " --- ";
        public string SellerName => CurrentEmployee?.PersonalDatum?.FirstName + " " + CurrentEmployee?.PersonalDatum?.LastName ?? " --- ";
        public Employee CurrentEmployee { get; private set; }

        public RelayCommand AddToReceiptCommand { get; set; }
        public RelayCommand DeleteFromReceiptCommand { get; set; }
        public RelayCommand ClearReceiptCommand { get; set; }
        public RelayCommand FinishReceiptCommand { get; set; }

        private PurchaseOrder _order;
        public PurchaseOrder Order 
        {
            get => _order; 
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order.PurchaseOrderProducts));
                OnPropertyChanged(nameof(Order));
            }
        }

        private ShopStorageProduct _selectedProductFromStorage;
        public ShopStorageProduct SelectedProductFromStorage 
        {
            get => _selectedProductFromStorage; 
            set
            {
                _selectedProductFromStorage = value;
                OnPropertyChanged(nameof(SelectedProductFromStorage));
            }
        }
        
        private PurchaseOrderProduct _selectedProductFromReceipt;
        public PurchaseOrderProduct SelectedProductFromReceipt
        {
            get => _selectedProductFromReceipt;
            set
            {
                _selectedProductFromReceipt = value;
                OnPropertyChanged(nameof(SelectedProductFromReceipt));
            }
        }

        private double _totalPriceForReceipt;
        public double TotalPriceForReceipt
        {
            get => _totalPriceForReceipt;
            set
            {
                _totalPriceForReceipt = value;
                OnPropertyChanged(nameof(TotalPriceForReceipt));
            }
        }

        public CreateReceiptViewModel(IAuthenticator authenticator, IReceiptService receiptService)
        {
            CurrentEmployee = authenticator.CurrentEmployee;
            httpAPIHelper = new HttpAPIHelper<ShopStorageProduct>();
            _receiptService = receiptService;

            ClearOrder();
            CreateCommands();
            LoadStorageItems();
            UpdateBindings();
        }

        public void CreateCommands()
        {
            AddToReceiptCommand = new RelayCommand(AddToReceipt, CanAddToReceipt);
            DeleteFromReceiptCommand = new RelayCommand(DeleteFromReceipt, CanDeleteFromReceipt);
            ClearReceiptCommand = new RelayCommand(ClearReceipt);
            FinishReceiptCommand = new RelayCommand(FinishReceipt);
        }

        public void ClearOrder()
        {
            Order = new PurchaseOrder();
            Order.Employee = CurrentEmployee;
            Order.EmployeeId = CurrentEmployee.EmployeeId;
            Order.DateTime = CurrentDate;
        }

        public void AddToReceipt(object? parameter)
        {
            ShopStorageProduct selectedShopStorageProduct = parameter as ShopStorageProduct;

            if (Order.PurchaseOrderProducts == null)
                Order.PurchaseOrderProducts = new List<PurchaseOrderProduct>();

            PurchaseOrderProduct prod = new PurchaseOrderProduct()
            {
                PurchaseOrder = Order,
                Product = new Product() { Title = SelectedProductFromStorage.Title },
                Price = (decimal) selectedShopStorageProduct.Price, 
                Quantity = 1,
                ProductId = selectedShopStorageProduct.ProductID,
            };

            if (Order.PurchaseOrderProducts.Where(x => x.ProductId == prod.ProductId).FirstOrDefault() == null)
            {
                Order.PurchaseOrderProducts.Add(prod);
                PurchaseOrderProducts = new ObservableCollection<PurchaseOrderProduct>(Order.PurchaseOrderProducts);
            }
            UpdateBindings();
        }

        public bool CanAddToReceipt(object? parameter)
        {
            if (parameter == null)
                return false;
            
            ShopStorageProduct selectedShopStorageProduct = parameter as ShopStorageProduct;
            if (selectedShopStorageProduct == null || selectedShopStorageProduct.Quantity <= 0)
                return false;

            UpdateBindings();
            return true;
        }

        public void DeleteFromReceipt(object? parameter)
        {
            PurchaseOrderProduct selectedShopStorageProduct = parameter as PurchaseOrderProduct;

            var prodToDelete = Order.PurchaseOrderProducts?.Select(x => x).Where(x => x.ProductId == selectedShopStorageProduct.ProductId).FirstOrDefault();
            if (prodToDelete != null)
            {
                Order.PurchaseOrderProducts?.Remove(prodToDelete);
                PurchaseOrderProducts = new ObservableCollection<PurchaseOrderProduct>(Order.PurchaseOrderProducts);
            }
            UpdateBindings();
        }

        public bool CanDeleteFromReceipt(object? parameter)
        {
            if (parameter == null)
                return false;

            PurchaseOrderProduct selectedShopStorageProduct = parameter as PurchaseOrderProduct;
            if (selectedShopStorageProduct == null)
                return false;

            return true;
        }

        public async void FinishReceipt(object? parameter)
        {
            if (MessageBox.Show("Do you want to save this receipt?", "Question", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Order.Employee = null;
                decimal total = 0;

                foreach (PurchaseOrderProduct prod in Order.PurchaseOrderProducts)
                {
                    total += prod.Price;
                    prod.PurchaseOrder = null;
                }
                Order.Price = total;

                if (await _receiptService.CreateReceipt(Order))
                {
                    //
                }
                else
                {
                    MessageBox.Show("Error during creating receipt", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ClearReceipt(object? parameter)
        {
            if (MessageBox.Show("Do you really want to clear the receipt?", "Question", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                ClearOrder();
                PurchaseOrderProducts = null;
                UpdateBindings();
            }
        }

        private async void LoadStorageItems()
        {
            try
            {
                IEnumerable<ShopStorageProduct> res = await httpAPIHelper.GetMultipleItemsRequest($"storages/employee?employeeID={CurrentEmployee.EmployeeId}");
                ShopStorageProducts = new ObservableCollection<ShopStorageProduct>(res);
                OnPropertyChanged(nameof(ShopStorageProducts));
                UpdateBindings();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void UpdateBindings()
        {
            base.UpdateBindings();
            OnPropertyChanged(nameof(PurchaseOrderProducts));
        }
    }
}
