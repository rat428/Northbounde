
using CommunityToolkit.Mvvm.ComponentModel;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section12ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private string carryOverDescription;

        public bool IsCarryOverComplete => CarryOverDescription?.Length >= 100;

        public Section12ViewModel()
        {
        }
    }
}