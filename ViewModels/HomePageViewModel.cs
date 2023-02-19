using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Timers;
using System.Windows;
using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using WpfApp1.Helpers;
using WpfApp1.Models;
using WpfApp1.MVVM;

namespace WpfApp1.ViewModels
{
    public partial class HomePageViewModel : NotificationObject
    {
        #region Properties
        public ObservableCollection<Worker> Workers { get; set; }

        public Worker? SelectedWorker
        {
            get => _selectedWorker;
            set => UpdatePropertyValue(ref _selectedWorker, value);
        }

        public ObservableTuple<string, DateOnly>? SelectedCertificate { get; set; }
        #endregion

        #region Ctor and OnAppExit
        public HomePageViewModel(MainWindowViewModel vm)
        {
            Workers = ReadWorkersFromJson() ?? new();
            _notificationTimer = new()
            {
                /* 
                   3600000 * 24
                   3600000      час
                   1800000      30 мин
                   1200000      20 мин
                   600000       10 мин
                */
                Interval = 1000,
                //AutoReset = false
            };
            _notificationTimer.Elapsed += OnTimedEvent;
            _notificationTimer.Start();
        }

        protected override void OnAppExit(object sender, ExitEventArgs e)
        {
            base.OnAppExit(sender, e);
            _notificationTimer.Stop();
            SaveWorkersToJson();
        }
        #endregion

        #region Methods for Commands

        #region Worker Commands
        private void AddWorker() => Workers.Add(new());

        private void ClearWorkersList() => Workers.Clear();

        private void RemoveSelectedWorker()
        {
            if (SelectedWorker is not null)
            {
                Workers.Remove(SelectedWorker);
            }
        }

        private void SaveWorkersToJson()
            => File.WriteAllText(_jsonPath, JsonConvert.SerializeObject(Workers));
        #endregion

        #region Certificates Commands
        private void AddCertificate()
            => SelectedWorker?.JobCertificates.Add(new("Новый сертификат", DateOnly.FromDateTime(DateTime.Today)));

        private void RemoveSelectedCertificate()
        {
            if (SelectedCertificate is not null)
            {
                SelectedWorker?.JobCertificates.Remove(SelectedCertificate);
            }
        }

        private void ClearCertificatesList()
            => SelectedWorker?.JobCertificates.Clear();

        #endregion

        #endregion

        #region Other methods
        private bool CheckWorkers(out List<Worker> badWorkers)
        {
            badWorkers = new();
            foreach (var worker in Workers)
            {
                if(worker.NeedAttention)
                {
                    badWorkers.Add(worker);
                }
            }
            return badWorkers.Count != 0;
        }

        private static void SendNotification(List<Worker> workers)
        {
            var notification = new ToastContentBuilder();
            string header = $"Необходимо продлить удостоверения:";

            notification.AddHeader(string.Empty, string.Empty, string.Empty)
                .AddText(header);
            foreach (var worker in workers)
            {
                var fullName = $"{worker.SecondName} {worker.FirstName} {worker.Patronymic}";
                notification.AddText(fullName);
            }
            notification.Show();
        }

        private void OnTimedEvent(object? source, ElapsedEventArgs e)
        {
            if (AppSettings.Settings?.NotificationsEnabled == true)
            {
                var minutesBetween = (DateTime.Now - _lastNotification).TotalMinutes;
                if (CheckWorkers(out var badWorkers)
                    && minutesBetween >= (int)AppSettings.Settings?.ToastInterval)
                {
                    SendNotification(badWorkers);
                    _lastNotification = DateTime.Now;
                }
            }
        }

        private static ObservableCollection<Worker>? ReadWorkersFromJson()
        {
            try
            {
                var json = File.ReadAllText(_jsonPath);
                return JsonConvert.DeserializeObject<ObservableCollection<Worker>>(json);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Fields
        private Worker? _selectedWorker;
        private const string _jsonPath = "workers.json";
        private readonly Timer _notificationTimer;
        private DateTime _lastNotification = DateTime.MinValue;
        #endregion
    }
}
