using System.Windows.Controls;

using GestioneCopie.WPF.App.ContentViewModels;

namespace GestioneCopie.WPF.App.Content
{
    /// <summary>
    /// Interaction logic for Sezioni.xaml
    /// </summary>
    public partial class Sezioni : UserControl
    {
        public Sezioni()
        {
            InitializeComponent();

            this.DataContext = new SezioniViewModel();
        }
    }
}
