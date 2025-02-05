using CommunityToolkit.Mvvm.ComponentModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section7ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private bool languageDisorderChecked;

        [ObservableProperty]
        private bool muscleWeaknessChecked;

        [ObservableProperty]
        private bool delayedMilestoneChecked;

        public Section7ViewModel() : base("Diagnosis")
        {

        }
    }
}
