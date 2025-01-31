using Northboundei.Mobile.Mvvm.ViewModels.Sections;

namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section2 : ContentView
{
	public Section2(Section2ViewModel section2)
    {
		InitializeComponent();
        BindingContext = section2;
    }

    private void OnAttendanceTypeChanged(object sender, EventArgs e)
    {

    }
}