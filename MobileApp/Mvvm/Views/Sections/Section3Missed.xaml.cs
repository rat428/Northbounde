using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section3Missed : ContentView
{
	public Section3Missed(Section3MissedViewModel section3)
    {
		InitializeComponent();
        BindingContext = section3;
    }
}