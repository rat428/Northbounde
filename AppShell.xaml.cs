using Northboundei.Mobile.Mvvm.ViewModels;
using Northboundei.Mobile.Mvvm.Views;

namespace Northboundei.Mobile
{
    public partial class AppShell : Shell
    {
        HomeViewModel _viewModel;

        public AppShell(HomeViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

  
    }
}
