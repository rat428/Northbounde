using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section10ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<Outcome> outcomes;
        public Section10ViewModel() : base("Outcomes and Objectives")
        {
            Outcomes = new ObservableCollection<Outcome>
            {
                new Outcome
                {
                    Title = "Test",
                    IsChecked = false,
                    Objectives = new ObservableCollection<Objective>
                    {
                        new Objective{ IsChecked = false, Title = "TestInside"}
                    }
                }
            };
        
        }

        override public void Validate()
        {
            // Always complete
            Complete = true;
        }
    }

    public partial class Outcome : ObservableObject
    {
        private bool _isChecked;

        public string Title { get; set; }
        public ObservableCollection<Objective> Objectives { get; set; }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                SetProperty(ref _isChecked, value);
                OnPropertyChanged(nameof(IsObjectiveVisible));
            }
        }

        public bool IsObjectiveVisible => IsChecked;
    }

    public class Objective : ObservableObject
    {
        public string Title { get; set; }
        public bool IsChecked { get; set; }
    }
}
