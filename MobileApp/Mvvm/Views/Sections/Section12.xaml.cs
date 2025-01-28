
namespace Northboundei.Mobile.Mvvm.Views.Sections;

public partial class Section12 : ContentView
{
    public Section12()
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