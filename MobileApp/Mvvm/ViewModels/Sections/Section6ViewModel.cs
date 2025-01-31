using CommunityToolkit.Mvvm.ComponentModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section6ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private bool languageDisorderChecked;

        [ObservableProperty]
        private bool muscleWeaknessChecked;

        [ObservableProperty]
        private bool delayedMilestoneChecked;

        public Section6ViewModel() : base("Diagnosis")
        {

        }
    }
}
