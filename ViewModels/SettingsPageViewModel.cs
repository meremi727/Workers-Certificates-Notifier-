using System.Reflection;
using WpfApp1.Models;
using WpfApp1.MVVM;

namespace WpfApp1.ViewModels
{
    public partial class SettingsPageViewModel : NotificationObject
    {
        private void ChangeInterval(NotificationsInterval newInterval)
        {
            if(AppSettings.Settings is not null)
            {
                AppSettings.Settings.Interval = newInterval;
            }
        }
            
    }
}
