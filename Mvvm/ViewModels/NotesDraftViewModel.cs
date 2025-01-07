using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Northboundei.Mobile.Mvvm.ViewModels
{
    public class NotesDraftViewModel : ObservableObject
    {
        public ICommand SaveDraftCommand => new Command(OnSaveDraftAction);

        public NotesDraftViewModel()
        {

        }


        private void OnSaveDraftAction(object obj)
        {
            
        }
    }
}
