﻿using CourseWorkApplication.State.Authentificators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel.Factories
{
    public class CheckStorageViewModelFactory : IViewModelFactory<CheckStorageViewModel>
    {
        private readonly IAuthenticator _authenticator;

        public CheckStorageViewModelFactory(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }

        public CheckStorageViewModel CreateViewModel()
        {
            return new CheckStorageViewModel(_authenticator);
        }
    }
}
