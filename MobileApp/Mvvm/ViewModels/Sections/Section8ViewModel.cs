using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section8ViewModel : SectionViewModelBase
    {

        [ObservableProperty]
        private bool isParentCaregiverSelected;

        [ObservableProperty]
        private bool isOtherSelected;

        [ObservableProperty]
        private string? otherText;


        [ObservableProperty]
        private string? communicationDetails;

        public bool IsOtherTextBoxVisible => IsOtherSelected;

        public bool IsCommunicationSectionVisible => IsOtherSelected && !IsParentCaregiverSelected;



        public Section8ViewModel()
        {

        }


        partial void OnIsOtherSelectedChanged(bool value)
        {
            OnPropertyChanged(nameof(IsOtherTextBoxVisible));
            OnPropertyChanged(nameof(IsCommunicationSectionVisible));
        }

        partial void OnIsParentCaregiverSelectedChanged(bool value)
        {
            OnPropertyChanged(nameof(IsCommunicationSectionVisible));
        }
    }
}
