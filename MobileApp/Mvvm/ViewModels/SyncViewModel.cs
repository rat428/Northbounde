using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Northboundei.Mobile.Database;
using Northboundei.Mobile.Database.Models;
using Northboundei.Mobile.Dto;
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
        private readonly IServiceAuthService _serviceAuthService;

        [ObservableProperty]
        ObservableCollection<SyncRecord> _syncRecords = new();
        public SyncViewModel(INoteService noteService, IServiceAuthService childService)
        {
            _noteService = noteService;
            _serviceAuthService = childService;

            FetchSyncHistory().ConfigureAwait(false);
        }

        [RelayCommand]
        public async Task SyncNow()
        {
            // Internet connection check
            if (!Connectivity.NetworkAccess.Equals(NetworkAccess.Internet))
            {
                await Shell.Current.DisplayAlert("No Internet Connection", "Please check your internet connection and try again.", "OK!");
                return;
            }

            try
            {
                IsBusy = true;

                await SyncData();

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

        private async Task SyncData()
        {
            await Push();

            await Pull();
        }

        private async Task Push()
        {
            var notes = await _noteService.GetNotesAsync(Offline: false);
            var serviceAuths = await _serviceAuthService.GetServiceAuthDataAsync(Offline: false);
            var notesDiff = notes.Except(await DatabaseService.GetAllDataAsync<SessionNoteData>());
            if (notesDiff.Any())
            {
                // TODO: Push notes to server
                
                // foreach (var note in notesDiff)
                // {
                //     await _noteService.AddNoteAsync(note);
                // }
            }
        }

        private async Task Pull()
        {
            // Clear Sync Tables
            await DatabaseService.ClearSyncTables();

            // Notes
            var notes = await _noteService.GetNotesAsync(Offline: false);
            await DatabaseService.AddAllAsync(notes!);

            // ServiceAuths
            var serviceAuths = await _serviceAuthService.GetServiceAuthDataAsync(Offline: false);
            await DatabaseService.AddAllAsync(serviceAuths!);
        }
    }
}
