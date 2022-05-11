using CourseWorkApplication.Services.ReceiptServices;
using CourseWorkApplication.Services.StoragesServices;
using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CreateReceiptViewModelFactory : IViewModelFactory<CreateReceiptViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly IReceiptService _receiptService;
        private readonly IStorageService _storageService;

        public CreateReceiptViewModelFactory(IAuthenticator authenticator, IReceiptService receiptService, IStorageService storageService)
        {
            _authenticator = authenticator;
            _receiptService = receiptService;
            _storageService = storageService;
        }

        public CreateReceiptViewModel CreateViewModel()
        {
            return new CreateReceiptViewModel(_authenticator, _receiptService, _storageService);
        }
    }
}
