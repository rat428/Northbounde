using Northboundei.Mobile.Mvvm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Mvvm.ViewModels
{
    public class SettingsViewModel:BaseViewModel
    {
        public SettingsModel Settings { get; set; } = new SettingsModel();

    }
}
