using Northboundei.Mobile.Mvvm.ViewModels;

namespace Northboundei.Mobile.Mvvm.Views;

public partial class SyncPage : ContentPage
{
	public SyncPage(SyncViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}