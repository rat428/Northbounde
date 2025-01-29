namespace Northboundei.Mobile.Mvvm.Views;

public partial class ActionPage : ContentPage
{
	public ActionPage()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs e)
    {
        AppShell._publichomeviewModel.UpdateLastSyncTimeCommand.Execute(null);
    }
}