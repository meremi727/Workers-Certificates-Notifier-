using System.ComponentModel;

namespace WpfApp1.Models
{
    public enum ToastNotificationInterval
    {
#if DEBUG
        [Description("1 минута (DEBUG)")]
        Minute = 1,
#endif
        [Description("30 минут")]
        HalfHour = 30,

        [Description("1 час")]
        OneHour = 60,

        [Description("2 часа")]
        TwoHours = 120,

        [Description("6 часов")]
        SixHours = 360,

        [Description("12 часов")]
        TwelveHours = 720
    }
}
