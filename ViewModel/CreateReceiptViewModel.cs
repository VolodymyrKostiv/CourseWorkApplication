using CourseWorkApplication.Commands;
using CourseWorkApplication.DTOs;
using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.Services.ProductServices;
using CourseWorkApplication.Services.ReceiptServices;
using CourseWorkApplication.Services.StoragesServices;
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
        private ObservableCollection<ShopStorageProduct> _shopStorageProducts;
        public ObservableCollection<ShopStorageProduct> ShopStorageProducts 
        {
            get => _shopStorageProducts;
            set
            {
                _shopStorageProducts = value;
                OnPropertyChanged(nameof(ShopStorageProducts));
            }
        }

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
        
        private readonly IReceiptService _receiptService;
        private readonly IStorageService _storageService;

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

        public CreateReceiptViewModel(IAuthenticator authenticator, IReceiptService receiptService, IStorageService storageService)
        {
            CurrentEmployee = authenticator.CurrentEmployee;
            _storageService = storageService;
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
            {
                Order.PurchaseOrderProducts = new List<PurchaseOrderProduct>();
            }

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
            if (Order.PurchaseOrderProducts.Count != 0 && MessageBox.Show("Do you want to save this receipt?", "Question", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                decimal total = 0;
                PurchaseOrderDTO purchaseOrderDTO = new PurchaseOrderDTO() 
                { 
                    DateTime = Order.DateTime, 
                    EmployeeId = Order.EmployeeId
                };

                foreach (PurchaseOrderProduct prod in Order.PurchaseOrderProducts)
                {
                    total += prod.Price;
                    PurchaseOrderProductDTO orderProductDTO = new PurchaseOrderProductDTO() 
                    { 
                        ProductId = prod.ProductId, 
                        Quantity = prod.Quantity,
                        Price = prod.Price,
                    };
                    (purchaseOrderDTO.PurchaseOrderProducts as HashSet<PurchaseOrderProductDTO>).Add(orderProductDTO);
                }

                Order.Price = total;
                purchaseOrderDTO.Price = Order.Price; 

                if (await _receiptService.CreateReceipt(purchaseOrderDTO))
                {
                    MessageBox.Show("Receipt successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateBindings();
                }
                else
                {
                    MessageBox.Show("Error during creating receipt", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ClearReceipt(object? parameter)
        {
            if ((parameter != null && (bool)parameter == true) || MessageBox.Show("Do you really want to clear the receipt?", "Question", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                ClearOrder();
                PurchaseOrderProducts = null;
                UpdateBindings();
            }
        }

        private async Task LoadStorageItems()
        {
            IEnumerable<ShopStorageProduct> res = await _storageService.LoadProductsFromStorage(CurrentEmployee.EmployeeId);
            ShopStorageProducts = new ObservableCollection<ShopStorageProduct>(res);
            OnPropertyChanged(nameof(ShopStorageProducts));
            UpdateBindings();
        }

        public override void UpdateBindings()
        {
            base.UpdateBindings();
            OnPropertyChanged(nameof(PurchaseOrderProducts));
            OnPropertyChanged(nameof(ShopStorageProducts));
        }
    }
}
