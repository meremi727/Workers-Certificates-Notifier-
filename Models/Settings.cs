using WpfApp1.MVVM;

namespace WpfApp1.Models
{

    public class Settings : NotificationObject
    {
        public bool NotificationsEnabled
        {
            get => _notificationsEnabled;
            set => UpdatePropertyValue(ref _notificationsEnabled, value);
        }

        public NotificationsInterval Interval
        {
            get => _interval;
            set => UpdatePropertyValue(ref _interval, value);
        }

        public ToastNotificationInterval ToastInterval 
        {
            get => _toastInterval; 
            set =>UpdatePropertyValue(ref _toastInterval, value);
        }

        public bool MarkWorkersWithExpiredCertificates
        {
            get => _markWorkersWithExpiredCertificates;
            set => UpdatePropertyValue(ref _markWorkersWithExpiredCertificates, value);
        }

        public bool AddToStartup
        {
            get => _addToStartup;
            set => UpdatePropertyValue(ref _addToStartup, value);
        }

        public Settings()
        {
            NotificationsEnabled = true;
            Interval = NotificationsInterval.OneWeek;
            ToastInterval = ToastNotificationInterval.TwoHours;
        }

        private bool _notificationsEnabled;
        private NotificationsInterval _interval;
        private bool _markWorkersWithExpiredCertificates;
        private bool _addToStartup;
        private ToastNotificationInterval _toastInterval;
    }
}
