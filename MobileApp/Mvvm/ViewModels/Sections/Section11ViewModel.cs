using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;

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
        [NotifyPropertyChangedFor(nameof(IsOtherValidationVisible))]
        private bool isOtherChecked;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsOtherValidationVisible))]
        private string otherDescription = string.Empty;

        public bool IsOtherValidationVisible => IsOtherChecked && string.IsNullOrWhiteSpace(OtherDescription);

        public Section11ViewModel() : base("Collaboration")
        {

        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            // Validate the other description field if the other checkbox is checked
            if (e.PropertyName == nameof(IsOtherValidationVisible))
            {
                if (IsOtherValidationVisible)
                {
                    HasError = true;
                    Complete = false;
                }
                else
                {
                    HasError = false;
                    Complete = true;
                }
            }
        }
    }
}
