using System.Windows.Controls;
using System.Windows.Media;

namespace GestioneCopie.Common.UI.ToolbarsControls
{
    /// <summary>
    /// Interaction logic for NewToolbarButton.xaml
    /// </summary>
    public partial class ToolbarButtonContent : UserControl
    {
        public ToolbarButtonContent()
        {
            InitializeComponent();
        }

        public Geometry PathData
        {
            set { this.IconPath.Data = value; }
        }
    }
}
