using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using Northboundei.Mobile.Mvvm.ViewModels;

namespace Northboundei.Mobile.Mvvm.Views;

public partial class LoginPage : ContentPage
{
    LoginViewModel _viewModel;
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }
}