using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Database.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.ViewModels
{
    public partial class SyncViewModel : BaseViewModel
    {
        [ObservableProperty]
        ObservableCollection<SyncRecord> _syncRecords;
        [RelayCommand]
        async Task SyncNow()
        {
            try
            {
                IsBusy = true;

                await Task.Delay(3000);

                await DatabaseService.AddDataAsync<SyncRecord>(new SyncRecord
                {
                    SyncDate = DateTime.Now
                });

                var syncRecords = await DatabaseService.GetAllDataAsync<SyncRecord>();
                _syncRecords = new ObservableCollection<SyncRecord>(syncRecords);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
