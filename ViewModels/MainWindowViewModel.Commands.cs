using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

using WpfApp1.MVVM;

namespace WpfApp1.ViewModels
{
    public partial class MainWindowViewModel
    {
        public MainWindowViewModelCammands Commands => _commands ??= new MainWindowViewModelCammands(this);

        public sealed class MainWindowViewModelCammands
        {
            public ICommand OpenHomePage { get; init; }
            public ICommand OpenSettingsPage { get; init; }
            public ICommand OpenHelpPage { get; init; }
            public ICommand OpenAboutPage { get; init; }
            public ICommand OpenWindow { get; init; }

            public ICommand CloseProgram { get; init; }

            public MainWindowViewModelCammands(MainWindowViewModel vm)
            {
                OpenHomePage = new DelegateCommand<RoutedEventArgs>(vm.OpenHomePage);
                OpenSettingsPage = new DelegateCommand<RoutedEventArgs>(vm.OpenSettingsPage);
                OpenHelpPage = new DelegateCommand<RoutedEventArgs>(vm.OpenHelpPage);
                OpenAboutPage = new DelegateCommand<RoutedEventArgs>(vm.OpenAboutPage);
                CloseProgram = new DelegateCommand(vm.CloseProgram);
                OpenWindow = new DelegateCommand(vm.OpenWindow);
            }
        }

        private MainWindowViewModelCammands? _commands;
    }
}
