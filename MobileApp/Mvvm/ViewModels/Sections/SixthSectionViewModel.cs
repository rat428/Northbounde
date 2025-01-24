using CommunityToolkit.Mvvm.ComponentModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class SixthSectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool languageDisorderChecked;

        [ObservableProperty]
        private bool muscleWeaknessChecked;

        [ObservableProperty]
        private bool delayedMilestoneChecked;

        public SixthSectionViewModel()
        {

        }
    }
}
