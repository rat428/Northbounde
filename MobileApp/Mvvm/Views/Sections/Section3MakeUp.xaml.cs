using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section3MakeUp : ContentView
{
	public Section3MakeUp(Section3MakeUpViewModel section3)
    {
		InitializeComponent();
        BindingContext = section3;
    }
}