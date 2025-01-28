namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section10 : ContentView
{
	public Section10()
	{
		InitializeComponent();
	}

    private void OnEditorTextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Length < 100)
        {
            ValidationLabel.IsVisible = true;
        }
        else
        {
            ValidationLabel.IsVisible = false;
        }
    }
}