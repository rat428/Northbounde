namespace Northboundei.Mobile.Mvvm.Views;

public partial class ActionPage : ContentPage
{
	public ActionPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        AppShell._publichomeviewModel.SyncPageCommand.Execute(null);
    }
}