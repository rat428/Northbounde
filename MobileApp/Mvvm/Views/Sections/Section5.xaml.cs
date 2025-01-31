using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section5 : ContentView
{
	public Section5(Section5ViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}