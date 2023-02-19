using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using Windows.Foundation.Collections;
using WpfApp1.Helpers;
using WpfApp1.Models;
using WpfApp1.MVVM;
using WpfApp1.Pages;

namespace WpfApp1.ViewModels
{
    public partial class MainWindowViewModel : NotificationObject, IDisposable
    {
        #region Properties
        public Page ActivePage
        {
            get => _activePage;
            set => UpdatePropertyValue(ref _activePage, value);
        }

        public WindowState WindowState
        {
            get => _windowState;
            set
            {
                ShowInTaskBar = true;
                UpdatePropertyValue(ref _windowState, value);
                ShowInTaskBar = value != WindowState.Minimized;
            }
        }

        public bool ShowInTaskBar
        {
            get => _showInTaskBar;
            set => UpdatePropertyValue(ref _showInTaskBar, value);
        }

        public static string ApplicationVersion
            => $"v.{Assembly.GetExecutingAssembly().GetName().Version}";

        public static string ApplicationName => $"{Application.ResourceAssembly.GetName().Name}";
        #endregion

        #region Ctor and OnAppExit
        public MainWindowViewModel()
        {
            AppSettings.LoadSettingsFromJson(_settingsPath);
            ToastNotificationManagerCompat.OnActivated += NotificationActivatedHandler;
            _homePage = new(this);
            _activePage = _homePage;
        }

        protected override void OnAppExit(object sender, ExitEventArgs e)
        {
            base.OnAppExit(sender, e);
            ToastNotificationManagerCompat.OnActivated -= NotificationActivatedHandler;
            AppSettings.SaveSettingsToJson(_settingsPath);
        }
        #endregion

        #region Methods for Commands
        private void OpenHomePage(RoutedEventArgs e)
           => ActivePage = _homePage;

        private void OpenSettingsPage(RoutedEventArgs e)
           => ActivePage = GetChachedValue(_settingsPage);

        private void OpenHelpPage(RoutedEventArgs e)
           => ActivePage = GetChachedValue(_helpPage);

        private void OpenAboutPage(RoutedEventArgs e)
           => ActivePage = GetChachedValue(_aboutPage);

        private void OpenWindow()
            => WindowState = WindowState.Normal;
        private void CloseProgram()
            => Application.Current.Shutdown();
        #endregion

        #region Other methods

        public void NotificationActivatedHandler(ToastNotificationActivatedEventArgsCompat e)
        {
            // Obtain the arguments from the notification
            ToastArguments args = ToastArguments.Parse(e.Argument);

            // Obtain any user input (text boxes, menu selections) from the notification
            ValueSet userInput = e.UserInput;

            // Need to dispatch to UI thread if performing UI operations
            Application.Current.Dispatcher.Invoke(delegate
            {
                // TODO: Show the corresponding content
                MessageBox.Show("Toast activated. Args: " + e.Argument);
            });
            WindowState = WindowState.Normal;
        }

        private static T GetChachedValue<T>(WeakReferenceCache<T> cache)
            where T : class
        {
            using var cacheEntry = cache.GetEntry();
            return cacheEntry.Value;
        }
        #endregion

        #region Fields
        private Page _activePage;
        private WindowState _windowState = WindowState.Normal;
        private bool _showInTaskBar = true;
        private readonly HomePage _homePage;
        private readonly WeakReferenceCache<Page> _settingsPage = new(() => new SettingsPage());
        private readonly WeakReferenceCache<Page> _helpPage = new(() => new HelpPage());
        private readonly WeakReferenceCache<Page> _aboutPage = new(() => new AboutPage());
        private const string _settingsPath = "settings.json";
        #endregion
    }
}
