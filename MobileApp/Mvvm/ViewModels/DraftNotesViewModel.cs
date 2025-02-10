using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Northboundei.Mobile.Dto;
using Northboundei.Mobile.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels;

public partial class DraftNotesViewModel : BaseViewModel
{
    [ObservableProperty]
    ObservableCollection<DraftNoteDataItem> _sessionNoteData = new();

    INoteService _noteService;
    IServiceAuthService _serviceAuthService;
    public DraftNotesViewModel(INoteService noteService, IServiceAuthService serviceAuthService)
    {
        _noteService = noteService;
        _serviceAuthService = serviceAuthService;
    }

    [RelayCommand]
    public async Task LoadData()
    {
        try
        {
            IsBusy = true;
            var notes = await _noteService.GetAllDrafts();
            var serviceAuth = await _serviceAuthService.GetServiceAuthDataAsync();

            SessionNoteData = new ObservableCollection<DraftNoteDataItem>(notes.Select(n => new DraftNoteDataItem
            {
                SessionId = n.SessionId,
                SessionDate = DateTime.Parse(n.SessionDate, System.Globalization.CultureInfo.InvariantCulture),
                EiNumber = n.EiNumber!,
                ChildFullName = serviceAuth!.Where(s => s.NyeisId == n.EiNumber).FirstOrDefault()?.DisplayName!
            }));
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK!");
        }
        finally
        {
            IsBusy = false;
        }
    }
    [RelayCommand]
    public async Task Edit(DraftNoteDataItem item)
    {
        var allNotes = await _noteService.GetNotesAsync();
        var note = allNotes.Where(n => n.SessionId == item.SessionId).First();

        await Shell.Current.GoToAsync("NotesPage", new Dictionary<string, object>()
        {
            { "Draft", note }
        });
    }
}

public partial class DraftNoteDataItem : ObservableObject
{
    [ObservableProperty]
    string _sessionId = null!;
    [ObservableProperty]
    DateTime _sessionDate;
    [ObservableProperty]
    string _eiNumber = null!;
    [ObservableProperty]
    string _childFullName = null!;
}