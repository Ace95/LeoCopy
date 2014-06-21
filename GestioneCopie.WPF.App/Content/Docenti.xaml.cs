using System.Windows.Controls;

using GestioneCopie.WPF.App.ContentViewModels;

namespace GestioneCopie.WPF.App.Content
{
    /// <summary>
    /// Interaction logic for Docenti.xaml
    /// </summary>
    public partial class Docenti : UserControl
    {
        public Docenti()
        {
            InitializeComponent();

            this.DataContext = new DocentiViewModel();
        }
    }
}
