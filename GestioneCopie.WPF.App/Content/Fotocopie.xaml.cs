using System.Windows.Controls;
using GestioneCopie.WPF.App.ContentViewModels;

namespace GestioneCopie.WPF.App.Content
{
    /// <summary>
    /// Interaction logic for Fotocopie.xaml
    /// </summary>
    public partial class Fotocopie : UserControl
    {
        public Fotocopie()
        {
            InitializeComponent();

            this.DataContext = new FotocopieViewModel();
        }
    }
}
