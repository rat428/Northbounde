using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section3CoVisitViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private string _covisitWhoElse;
        [ObservableProperty]
        private string _covisitWhoElseRelationship;
        [ObservableProperty]
        private string _authorizedCovisits;
        public Section3CoVisitViewModel() : base("Co-Visit Attendance")
        {

        }

        public override void Validate()
        {
            Complete = !string.IsNullOrEmpty(CovisitWhoElse) && AuthorizedCovisits != "0";
        }
    }
}
