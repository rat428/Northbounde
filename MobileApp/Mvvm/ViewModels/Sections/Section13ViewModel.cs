
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

        public override void Validate()
        {
            Complete = !IsCarryOverValidationVisible;
        }
    }
}