
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Mvvm.ViewModels.Sections;
using Northboundei.Mobile.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections;

public partial class SectionViewModelBase : ObservableObject
{
    [ObservableProperty]
    string _sectionTitle = string.Empty;

    // Has Error and Is Complete
    [ObservableProperty]
    bool _hasError = false;
    [ObservableProperty]
    bool _complete = false;

    [ObservableProperty]
    bool _isBusy = false;

    public SectionViewModelBase(string Title)
    {
        SectionTitle = Title;
    }
}

// Color Converter for Status of a Section
public class SectionStatusColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Takes in the Section's Page, Infers the SectionViewModel, and returns the color based on the Section's Status
        if (value is ContentPage page)
        {
            var viewModel = page.BindingContext as SectionViewModelBase;
            if (viewModel != null)
            {
                if (viewModel.Complete)
                {
                    return Colors.GreenYellow;
                }
                else if (viewModel.HasError)
                {
                    return Colors.OrangeRed;
                }
            }
        }

        return Colors.YellowGreen;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}