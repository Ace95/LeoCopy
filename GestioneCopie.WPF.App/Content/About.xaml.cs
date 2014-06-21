using System.Windows.Controls;
using GestioneCopie.WPF.App.ContentViewModels;

namespace GestioneCopie.WPF.App.Content
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        

        public About()
        {
            InitializeComponent();

            this.DataContext = new AboutViewModel();
        }
    }
}
