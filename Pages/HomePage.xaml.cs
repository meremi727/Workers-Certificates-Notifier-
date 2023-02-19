using System;
using System.Windows.Controls;

using WpfApp1.ViewModels;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage( MainWindowViewModel vm)
        {
            InitializeComponent();
            DataContext = new HomePageViewModel(vm);
        }
    }
}
