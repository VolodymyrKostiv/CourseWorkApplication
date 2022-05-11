using CourseWorkApplication.Services.AuthentificationServices;
using CourseWorkApplication.Services.ProductServices;
using CourseWorkApplication.Services.ReceiptServices;
using CourseWorkApplication.Services.StoragesServices;
using CourseWorkApplication.Services.SupplierServices;
using CourseWorkApplication.Services.SupplyServices;
using CourseWorkApplication.State.Authentificators;
using CourseWorkApplication.State.Navigators;
using CourseWorkApplication.ViewModel;
using CourseWorkApplication.ViewModel.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CourseWorkApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateServiceProvider();

            Window window = new MainWindow();
            window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IAuthenticator, Authenticator>();

            services.AddSingleton<IAuthentificationService, AuthentificationService>();
            services.AddSingleton<IReceiptService, ReceiptService>();
            services.AddSingleton<ISupplyService, SupplyService>();
            services.AddSingleton<IStorageService, StorageService>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<ISupplierService, SupplierService>();

            services.AddSingleton<IViewModelAbstractFactory, ViewModelAbstractFactory>();
            services.AddSingleton<IViewModelFactory<CheckReceiptsViewModel>, CheckReceiptsViewModelFactory>();
            services.AddSingleton<IViewModelFactory<CheckStorageViewModel>, CheckStorageViewModelFactory>();
            services.AddSingleton<IViewModelFactory<CheckSupplyOrdersViewModel>, CheckSupplyOrdersViewModelFactory>();
            services.AddSingleton<IViewModelFactory<CreateReceiptViewModel>, CreateReceiptViewModelFactory>();
            services.AddSingleton<IViewModelFactory<CreateSupplyOrderViewModel>, CreateSupplyOrderViewModelFactory>();
            services.AddSingleton<IViewModelFactory<LoginViewModel>>((services) => 
                new LoginViewModelFactory(services.GetRequiredService<IAuthenticator>(), 
                new ViewModelFactoryRenavigator<CheckStorageViewModel>(services.GetRequiredService<INavigator>(),
                services.GetRequiredService<IViewModelFactory<CheckStorageViewModel>>()))); 

            services.AddScoped<MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
