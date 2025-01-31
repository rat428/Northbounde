
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section13ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCarryOverValidationVisible))]
        private string carryOverDescription = string.Empty;

        public bool IsCarryOverValidationVisible => CarryOverDescription.Length <= 100;

        public Section13ViewModel() : base("Carry-Over")
        {

        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            // Validate the carry-over description field
            if (e.PropertyName == nameof(IsCarryOverValidationVisible))
            {
                if (IsCarryOverValidationVisible)
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