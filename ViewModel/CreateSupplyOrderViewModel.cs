using CourseWorkApplication.Commands;
using CourseWorkApplication.DTOs;
using CourseWorkApplication.Helpers;
using CourseWorkApplication.Models;
using CourseWorkApplication.Services.ProductServices;
using CourseWorkApplication.Services.SupplierServices;
using CourseWorkApplication.Services.SupplyServices;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWorkApplication.ViewModel
{
    public class CreateSupplyOrderViewModel : ViewModelBase
    {
        #region Observable collections

        private ObservableCollection<FullProductInfo> _products;
        public ObservableCollection<FullProductInfo> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private ObservableCollection<SupplyOrderProduct> _supplyOrderProducts;
        public ObservableCollection<SupplyOrderProduct> SupplyOrderProducts
        {
            get => _supplyOrderProducts;
            set
            {
                _supplyOrderProducts = value;
                OnPropertyChanged(nameof(SupplyOrderProducts));
            }
        }

        #endregion Observable collections

        #region Services

        private IHttpAPIHelper<FullProductInfo> httpAPIHelper;
        private readonly ISupplyService _supplyService;
        private readonly IProductService _productService;
        private readonly ISupplierService _supplierService;

        #endregion Services

        #region Receipt Data

        public DateTime CurrentDate => DateTime.UtcNow;
        public string ShopAddress => CurrentEmployee?.Shop?.Address ?? " --- ";
        public string ManagerName => CurrentEmployee?.PersonalDatum?.FirstName + " " + CurrentEmployee?.PersonalDatum?.LastName ?? " --- ";
        public Employee CurrentEmployee { get; private set; }

        #endregion Receipt Data

        #region Commands

        public RelayCommand AddToSupplyOrderCommand { get; set; }
        public RelayCommand DeleteFromSupplyOrderCommand { get; set; }
        public RelayCommand ClearSupplyOrderCommand { get; set; }
        public RelayCommand FinishSupplyOrderCommand { get; set; }

        #endregion Commands

        private SupplyOrder _supplyOrder;
        public SupplyOrder Order
        {
            get => _supplyOrder;
            set
            {
                _supplyOrder = value;
                OnPropertyChanged(nameof(Order.SupplyOrderProducts));
                OnPropertyChanged(nameof(Order));
            }
        }

        #region Selected Table Items

        private FullProductInfo _selectedProduct;
        public FullProductInfo SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        private SupplyOrderProduct _selectedSupplyOrderProductFromOrder;
        public SupplyOrderProduct SelectedSupplyOrderProductFromOrder
        {
            get => _selectedSupplyOrderProductFromOrder;
            set
            {
                _selectedSupplyOrderProductFromOrder = value;
                OnPropertyChanged(nameof(SelectedSupplyOrderProductFromOrder));
            }
        }

        private ObservableCollection<SupplierDTO> _suppliers;
        public ObservableCollection<SupplierDTO> Suppliers
        {
            get => _suppliers;
            set
            {
                _suppliers = value;
                OnPropertyChanged(nameof(Suppliers));
            }
        }

        private SupplierDTO _selectedSupplier;
        public SupplierDTO SelectedSupplier
        {
            get => _selectedSupplier;
            set
            {
                _selectedSupplier = value;
                OnPropertyChanged(nameof(SelectedSupplier));
            }
        }

        #endregion Selected Table Items

        #region Constructor

        public CreateSupplyOrderViewModel(IAuthenticator authenticator, 
            ISupplyService supplyService, IProductService productService,
            ISupplierService supplierService)
        {
            CurrentEmployee = authenticator.CurrentEmployee;
            httpAPIHelper = new HttpAPIHelper<FullProductInfo>();
            _supplyService = supplyService;
            _productService = productService;
            _supplierService = supplierService;

            ResetOrder();
            CreateCommands();
            LoadProducts();
            LoadSuppliers();
            UpdateBindings();
        }

        #endregion Constructor

        #region Command Methods

        public void CreateCommands()
        {
            AddToSupplyOrderCommand = new RelayCommand(AddToOrder, CanAddToOrder);
            DeleteFromSupplyOrderCommand = new RelayCommand(DeleteFromOrder, CanDeleteFromOrder);
            ClearSupplyOrderCommand = new RelayCommand(ClearOrder);
            FinishSupplyOrderCommand = new RelayCommand(FinishOrder);
        }

        public void ResetOrder()
        {
            Order = new SupplyOrder();
            Order.Employee = CurrentEmployee;
            Order.EmployeeId = CurrentEmployee.EmployeeId;
            Order.RequestDate = CurrentDate;
        }

        public void AddToOrder(object? parameter)
        {
            FullProductInfo selectedProduct = parameter as FullProductInfo;

            if (Order.SupplyOrderProducts == null)
            {
                Order.SupplyOrderProducts = new List<SupplyOrderProduct>();
            }

            SupplyOrderProduct supplyOrderProduct = new SupplyOrderProduct()
            {
                SupplyOrder = Order,
                Product = new Product() { Title = selectedProduct.Title },
                Quantity = 1,
                ProductId = selectedProduct.ProductId,
            };
            
            if (Order.SupplyOrderProducts.Where(x => x.ProductId == supplyOrderProduct.ProductId).FirstOrDefault() == null)
            {
                Order.SupplyOrderProducts.Add(supplyOrderProduct);
                SupplyOrderProducts = new ObservableCollection<SupplyOrderProduct>(Order.SupplyOrderProducts);
            }
            UpdateBindings();
        }

        public bool CanAddToOrder(object? parameter)
        {
            if (parameter == null)
                return false;

            FullProductInfo selectedProduct = parameter as FullProductInfo;
            if (selectedProduct == null || Order == null)
                return false;

            UpdateBindings();
            return true;
        }

        public void DeleteFromOrder(object? parameter)
        {
            SupplyOrderProduct selectedSupplyOrderProduct = parameter as SupplyOrderProduct;

            var prodToDelete = Order.SupplyOrderProducts.Select(x => x).Where(x => x.ProductId == selectedSupplyOrderProduct.ProductId).FirstOrDefault();
            if (prodToDelete != null)
            {
                Order.SupplyOrderProducts?.Remove(prodToDelete);
                SupplyOrderProducts = new ObservableCollection<SupplyOrderProduct>(Order.SupplyOrderProducts);
            }
            UpdateBindings();
        }

        public bool CanDeleteFromOrder(object? parameter)
        {
            if (parameter == null)
                return false;

            SupplyOrderProduct selectedSupplyOrderProduct = parameter as SupplyOrderProduct;
            if (selectedSupplyOrderProduct == null || Order == null || Order.SupplyOrderProducts == null)
                return false;

            return true;
        }

        public async void FinishOrder(object? parameter)
        {
            if (SelectedSupplier != null && Order.SupplyOrderProducts.Count != 0 && MessageBox.Show("Do you want to save this order?", "Question", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                SupplyOrderDTO supplyOrderDTO = new SupplyOrderDTO()
                {
                    RequestDate = CurrentDate,
                    EmployeeId = Order.EmployeeId,
                    SupplierId = SelectedSupplier.SupplierId,
                    TotalPrice = 0,
                    Supplier = SelectedSupplier,
                    SupplyOrderState = new SupplyOrderStateDTO() { SupplyOrderStateId = 0, Title = string.Empty },
                };

                foreach (SupplyOrderProduct prod in Order.SupplyOrderProducts)
                {
                    SupplyOrderProductDTO supplyOrderProductDTO = new SupplyOrderProductDTO()
                    {
                        ProductId = prod.ProductId,
                        Quantity = prod.Quantity,
                    };
                    (supplyOrderDTO.SupplyOrderProducts as HashSet<SupplyOrderProductDTO>).Add(supplyOrderProductDTO);
                }

                if (await _supplyService.CreateSupplyOrder(supplyOrderDTO))
                {
                    MessageBox.Show("Supply order successfully created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetOrder();
                    UpdateBindings();
                }
                else
                {
                    MessageBox.Show("Error during creating supply order", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void ClearOrder(object? parameter)
        {
            if ((parameter != null && (bool)parameter == true) || MessageBox.Show("Do you really want to clear the order?", "Question", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                ResetOrder();
                SupplyOrderProducts = null;
                UpdateBindings();
            }
        }

        #endregion Command Methods

        #region Methods

        private async Task LoadProducts()
        {
            try
            {
                IEnumerable<FullProductInfo> res = await _productService.GetAllProductsInfo();
                Products = new ObservableCollection<FullProductInfo>(res);
                OnPropertyChanged(nameof(Products));
                UpdateBindings();
            }
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task LoadSuppliers()
        {
            try
            {
                IEnumerable<SupplierDTO> res = await _supplierService.GetAllSuppliers();
                Suppliers = new ObservableCollection<SupplierDTO>(res);
                OnPropertyChanged(nameof(Suppliers));
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
            OnPropertyChanged(nameof(Products));
            OnPropertyChanged(nameof(SupplyOrderProducts));
            OnPropertyChanged(nameof(Suppliers));
        }

        #endregion Methods
    }
}
