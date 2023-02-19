using System.Windows.Input;
using WpfApp1.MVVM;

namespace WpfApp1.ViewModels
{
	public partial class HomePageViewModel
	{
		public HomePageViewModelCommands Commands => _commands ??= new HomePageViewModelCommands(this);
		public sealed class HomePageViewModelCommands
        {
            #region Worker Commands
            public ICommand AddWorker { get; init; }
			public ICommand RemoveSelectedWorker { get; init; }
			public ICommand UpdateWorkersFile { get; init; }
			public ICommand ClearWorkersList { get; init; }
            #endregion

            #region Certificates Commands
            public ICommand AddCertificate { get; init; }
            public ICommand RemoveSelectedCertificate { get; init; }
            public ICommand ClearCertificatesList { get; init; }
            #endregion
            public HomePageViewModelCommands(HomePageViewModel vm)
			{
				AddWorker = new DelegateCommand(vm.AddWorker);
				RemoveSelectedWorker = new DelegateCommand(vm.RemoveSelectedWorker);
				UpdateWorkersFile = new DelegateCommand(vm.SaveWorkersToJson);
				AddCertificate = new DelegateCommand(vm.AddCertificate);
				ClearWorkersList = new DelegateCommand(vm.ClearWorkersList);
				AddCertificate= new DelegateCommand(vm.AddCertificate);
				RemoveSelectedCertificate = new DelegateCommand(vm.RemoveSelectedCertificate);
				ClearCertificatesList = new DelegateCommand(vm.ClearCertificatesList);
			}
		}
		private HomePageViewModelCommands? _commands;
	}
}
