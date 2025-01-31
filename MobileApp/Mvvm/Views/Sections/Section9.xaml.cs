using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section9 : ContentView
{
	public Section9(Section9ViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}