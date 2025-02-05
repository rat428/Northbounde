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
        ObservableCollection<SyncRecord> _syncRecords = new();

        public SyncViewModel()
        {
            FetchSyncHistory();
        }

        [RelayCommand]
        public async Task SyncNow()
        {
            try
            {
                IsBusy = true;


                await DatabaseService.AddDataAsync<SyncRecord>(new SyncRecord
                {
                    SyncDate = DateTime.Now
                });
                
                await Task.Delay(3000);

                await FetchSyncHistory();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task FetchSyncHistory()
        {
            var syncRecords = await DatabaseService.GetAllDataAsync<SyncRecord>();
            SyncRecords.Clear();
            foreach (var record in syncRecords)
            {
                SyncRecords.Insert(0,record);
            }
        }
    }
}
