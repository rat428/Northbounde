using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section3CoVisit : ContentView
{
	public Section3CoVisit(Section3CoVisitViewModel section3)
    {
		InitializeComponent();
        BindingContext = section3;
    }
}