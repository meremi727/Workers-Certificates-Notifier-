using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Windows.Security.Cryptography.Certificates;
using WpfApp1.Helpers;
using WpfApp1.MVVM;

namespace WpfApp1.Models
{
    public class Worker : NotificationObject
    {
        #region Properties
        public string FirstName
        {
            get => _firstName;
            set => UpdatePropertyValue(ref _firstName, value);
        }

        public string SecondName
        {
            get => _secondName;
            set => UpdatePropertyValue(ref _secondName, value);
        }

        public string Patronymic
        {
            get => _patronymic;
            set => UpdatePropertyValue(ref _patronymic, value);
        }

        public bool NeedAttention
        {
            get
            {
                var value = CheckCertificates();
                UpdatePropertyValue(ref _needAttention, value);
                return value;
            }
            set => UpdatePropertyValue(ref _needAttention,value);
        }

        public ObservableCollection<ObservableTuple<string, DateOnly>> JobCertificates { get; set; }
        #endregion

        #region Ctor
        public Worker(string firstName = "Имя",
              string secondName = "Фамилия",
              string patronymic = "Отчество")
        {
            _firstName = firstName;
            _secondName = secondName;
            _patronymic = patronymic;
            JobCertificates = new();
        }
        #endregion

        #region Methods
        private bool CheckCertificates()
        {
            var now = DateTime.Now;
            foreach (var cert in JobCertificates)
            {
                var year = cert.Item2.Year;
                var month = cert.Item2.Month;
                var day = cert.Item2.Day;
                DateTime certDate = new(year, month, day);
                TimeSpan span = certDate - now;
                if (span.TotalDays <= (int?)AppSettings.Settings?.Interval)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Fields
        private string _firstName;
        private string _secondName;
        private string _patronymic;
        private bool _needAttention;
        #endregion
    }
}