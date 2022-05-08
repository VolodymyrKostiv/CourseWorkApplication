using CourseWorkApplication.Services.ReceiptServices;
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

        public CreateReceiptViewModelFactory(IAuthenticator authenticator, IReceiptService receiptService)
        {
            _authenticator = authenticator;
            _receiptService = receiptService;
        }

        public CreateReceiptViewModel CreateViewModel()
        {
            return new CreateReceiptViewModel(_authenticator, _receiptService);
        }
    }
}
