using System.ComponentModel;

namespace WpfApp1.Models
{
    public enum NotificationsInterval
    {
        [Description ("1 день")]
        OneDay = 1,

        [Description ("3 дня")]
        ThreeDays = 3,

        [Description ("1 неделю")]
        OneWeek = 7,

        [Description ("2 недели")]
        TwoWeeks = 14,

        [Description ("1 месяц (30 дней)")]
        OneMonth = 30,

        [Description ("2 месяца (60 дней)")]
        TwoMonths = 60
    }
}
