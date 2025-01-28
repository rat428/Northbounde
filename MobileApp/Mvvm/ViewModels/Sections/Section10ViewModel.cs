using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section10ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private string sessionDescription;

        public bool IsDescriptionComplete => SessionDescription?.Length >= 100;

        public Section10ViewModel()
        {
        }
    }
}
