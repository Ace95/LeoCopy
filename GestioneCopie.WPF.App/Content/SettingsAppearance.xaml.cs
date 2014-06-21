using System.Windows.Controls;
using GestioneCopie.WPF.App.ContentViewModels;

namespace GestioneCopie.WPF.App.Content
{
    /// <summary>
    /// Interaction logic for SettingsAppearance.xaml
    /// </summary>
    public partial class SettingsAppearance : UserControl
    {
        public SettingsAppearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            this.DataContext = new SettingsAppearanceViewModel();
        }
    }
}
