using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section11ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private bool isObservedChecked;

        [ObservableProperty]
        private bool isTriedActivityChecked;

        [ObservableProperty]
        private bool isDemonstratedChecked;

        [ObservableProperty]
        private bool isReviewedChecked;

        [ObservableProperty]
        private bool isOtherChecked;

        [ObservableProperty]
        private string otherDescription;

        public bool IsOtherValidationVisible => IsOtherChecked && string.IsNullOrWhiteSpace(OtherDescription);

        public Section11ViewModel()
        {
        }
    }
}
