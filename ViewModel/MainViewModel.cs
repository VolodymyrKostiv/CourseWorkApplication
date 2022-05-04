using CourseWorkApplication.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWorkApplication.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public bool IsSeller { get; set; } = true;
        public INavigator Navigator { get; set; } = new Navigator();
    }
}
