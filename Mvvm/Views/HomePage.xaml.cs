using Northboundei.Mobile.Mvvm.ViewModels;

namespace Northboundei.Mobile.Mvvm.Views;

public partial class HomePage : ContentPage
{

    HomeViewModel _viewModel;
    public HomePage(HomeViewModel viewModel)
    {

        InitializeComponent();
        BindingContext = viewModel;
        Loaded += HomePage_Loaded;
        _viewModel = viewModel;
    }

    private void HomePage_Loaded(object? sender, EventArgs e)
    {
        _viewModel.LoadCommand.Execute(null);
    }

    protected override void OnAppearing()
    {
        _viewModel.StartTimer();
        base.OnAppearing();
    }
    protected override void OnDisappearing()
    {
        _viewModel.StopTimer();
        base.OnAppearing();
    }
}