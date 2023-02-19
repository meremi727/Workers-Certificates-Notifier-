using System;
using System.Windows;

namespace WpfApp1.Helpers
{
    public abstract class ApplicationExitHandler : IDisposable
    {
        protected ApplicationExitHandler() 
            => Application.Current.Exit += this.OnAppExit;

        ~ApplicationExitHandler() 
            => Application.Current.Exit -= this.OnAppExit;

        public virtual void Dispose()
        {
            Application.Current.Exit -= this.OnAppExit;
            GC.SuppressFinalize(this);
        }

        protected virtual void OnAppExit(object sender, ExitEventArgs e) { }
    }
}
