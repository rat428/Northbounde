using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section10 : ContentView
{
	public Section10(Section10ViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}