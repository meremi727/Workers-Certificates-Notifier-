using System.Windows.Input;
using WpfApp1.Models;
using WpfApp1.MVVM;

namespace WpfApp1.ViewModels
{
    public partial class SettingsPageViewModel
    {
        public SettingsPageViewModelCommands Commands => _commands ??= new SettingsPageViewModelCommands(this);
        public sealed class SettingsPageViewModelCommands
        {
            public ICommand ChangeInterval { get; init; }

            public SettingsPageViewModelCommands(SettingsPageViewModel vm)
            {
                ChangeInterval = new DelegateCommand<NotificationsInterval>(vm.ChangeInterval);
            }
        }

        private SettingsPageViewModelCommands? _commands;
    }
}
