using System.Windows.Controls;

using GestioneCopie.WPF.App.ContentViewModels;

namespace GestioneCopie.WPF.App.Content
{
    /// <summary>
    /// Interaction logic for Classi.xaml
    /// </summary>
    public partial class Classi : UserControl
    {
        public Classi()
        {
            InitializeComponent();

            this.DataContext = new ClassiViewModel();
        }
    }
}
