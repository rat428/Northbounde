using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section7 : ContentView
{
	public Section7(Section7ViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}