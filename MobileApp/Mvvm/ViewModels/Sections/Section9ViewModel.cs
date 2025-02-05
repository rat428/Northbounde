using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section9ViewModel : SectionViewModelBase
    {

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCommunicationSectionVisible))]
        private bool isParentCaregiverSelected;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsOtherTextBoxVisible))]
        [NotifyPropertyChangedFor(nameof(IsCommunicationSectionVisible))]
        private bool isOtherSelected;

        [ObservableProperty]
        private string? otherText;


        [ObservableProperty]
        private string? communicationDetails;

        public bool IsOtherTextBoxVisible => IsOtherSelected;

        public bool IsCommunicationSectionVisible => IsOtherSelected && !IsParentCaregiverSelected;



        public Section9ViewModel() : base("Participants")
        {

        }

    }
}
