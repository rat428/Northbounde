using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Northboundei.Mobile.Mvvm.ViewModels;
public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    bool _isBusy = false;
}
