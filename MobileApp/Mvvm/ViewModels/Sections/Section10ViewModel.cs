﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels.Sections
{
    public partial class Section10ViewModel : SectionViewModelBase
    {
        [ObservableProperty]
        private string sessionDescription = string.Empty;

        [ObservableProperty]
        bool isDescValidationVisible = false;
        public Section10ViewModel() : base("Session Description")
        {
        }

        partial void OnSessionDescriptionChanged(string value)
        {
            if (SessionDescription.Length < 100)
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
