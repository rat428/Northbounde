using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section8 : ContentView
{
	public Section8(Section8ViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}