using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section6 : ContentView
{
	public Section6(Section6ViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}