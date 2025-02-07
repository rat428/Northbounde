using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Database.Models;
using Northboundei.Mobile.IServices;
using Northboundei.Mobile.Services;
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
        private readonly INoteService _noteService;
        private readonly IChildService _childService;

        [ObservableProperty]
        ObservableCollection<SyncRecord> _syncRecords = new();

        public SyncViewModel(INoteService noteService, IChildService childService)
        {
            _noteService = noteService;
            _childService = childService;

            FetchSyncHistory().ConfigureAwait(false);
        }

        [RelayCommand]
        public async Task SyncNow()
        {
            try
            {
                IsBusy = true;

                await InitializeData();


                // Will reach this part if the sync is successful
                await DatabaseService.AddDataAsync(new SyncRecord
                {
                    SyncDate = DateTime.Now
                });
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

        private async Task InitializeData()
        {
            if (!SessionManager.IsNotesSync)
            {
                var notes = await _noteService.GetNotesAsync();
                await SecureStorage.Default.SetAsync(SessionManager.UserContext.EncryptionKey + nameof(INoteService), JsonConvert.SerializeObject(notes)).ConfigureAwait(false); ;
                SessionManager.IsNotesSync = true;
            }
            if (!SessionManager.IsAuthSync)
            {
                var children = await _childService.GetChildrenAsync();
                await SecureStorage.Default.SetAsync(SessionManager.UserContext.EncryptionKey + nameof(IChildService), JsonConvert.SerializeObject(children)).ConfigureAwait(false);
                SessionManager.IsAuthSync = true;
            }
        }
    }
}
