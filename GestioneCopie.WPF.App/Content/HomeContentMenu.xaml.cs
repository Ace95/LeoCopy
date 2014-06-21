using System.Windows.Controls;
using GestioneCopie.WPF.App.ContentViewModels;

namespace GestioneCopie.WPF.App.Content
{
    /// <summary>
    /// Interaction logic for SettingsAppearance.xaml
    /// </summary>
    public partial class HomeContentMenu : UserControl
    {
        public HomeContentMenu()
        {
            InitializeComponent();

            // create and assign the appearance view model
            this.DataContext = new HomeContentMenuViewModel();
        }
    }
}
