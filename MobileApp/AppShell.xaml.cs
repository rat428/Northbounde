using Northboundei.Mobile.Mvvm.ViewModels;
using Northboundei.Mobile.Mvvm.Views;

namespace Northboundei.Mobile
{
    public partial class AppShell : Shell
    {
       public static HomeViewModel _publichomeviewModel;

        public AppShell(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _publichomeviewModel = viewModel;
        }
    }
}
