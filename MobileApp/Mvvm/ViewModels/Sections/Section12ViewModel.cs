
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section12ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCarryOverComplete))]
        private string carryOverDescription = string.Empty;

        public bool IsCarryOverComplete => CarryOverDescription.Length >= 100;

        public Section12ViewModel() : base("Carry-Over")
        {

        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            // Validate the carry-over description field
            if (e.PropertyName == nameof(IsCarryOverComplete))
            {
                if (IsCarryOverComplete)
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