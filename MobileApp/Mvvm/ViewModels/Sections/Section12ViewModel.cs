using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section12ViewModel : SectionViewModelBase
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
        [NotifyPropertyChangedFor(nameof(IsOtherValidationVisible))]
        private bool isOtherChecked;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsOtherValidationVisible))]
        private string otherDescription = string.Empty;

        public bool IsOtherValidationVisible => IsOtherChecked && string.IsNullOrWhiteSpace(OtherDescription);

        public string SelectedStrategies => string.Join(", ",
        [
            IsObservedChecked ? "Observed" : null,
            IsTriedActivityChecked ? "Tried Activity" : null,
            IsDemonstratedChecked ? "Demonstrated" : null,
            IsReviewedChecked ? "Reviewed" : null,
            IsOtherChecked ? OtherDescription : null
        ]);

        public Section12ViewModel() : base("Collaboration")
        {

        }

        public override void Validate()
        {
            Complete = !IsOtherValidationVisible;
        }
    }
}
