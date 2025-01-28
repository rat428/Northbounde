
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class SectionViewModelBase : ObservableObject
    {
        private static readonly List<SectionViewModelBase> instances = new();

        public SectionViewModelBase()
        {
            instances.Add(this);
        }

        [ObservableProperty]
        private bool isOpen = false;

        public bool Complete { get; private set; }

        partial void OnIsOpenChanged(bool value)
        {
            if (!value)
            {
                foreach (var vm in instances)
                {
                    if(vm != this)
                    {
                        vm.IsOpen = false;
                    }
                }
            }
        }
    }
}