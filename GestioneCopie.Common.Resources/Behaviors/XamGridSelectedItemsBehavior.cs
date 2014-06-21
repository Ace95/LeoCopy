using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using System.Collections.Specialized;
using System.Collections;

using Infragistics.Controls.Grids;

namespace GestioneCopie.Common.Resources.Behaviors
{
    public class XamGridSelectedItemsBehavior : Behavior<XamGrid>
    {
        private XamGrid Grid
        {
            get
            {
                return AssociatedObject as XamGrid;
            }
        }

        public INotifyCollectionChanged SelectedItems
        {
            get { return (INotifyCollectionChanged)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems",
            typeof(INotifyCollectionChanged),
            typeof(XamGridSelectedItemsBehavior),
            null);

        protected override void OnAttached()
        {
            base.OnAttached();

            Grid.SelectedRowsCollectionChanged += new System.EventHandler<SelectionCollectionChangedEventArgs<SelectedRowsCollection>>(Grid_SelectedRowsCollectionChanged);
        }

        void Grid_SelectedRowsCollectionChanged(object sender, SelectionCollectionChangedEventArgs<SelectedRowsCollection> e)
        {
            UnsubscribeFromEvents();

            TransferSourceToTarget(Grid.SelectionSettings.SelectedRows, this.SelectedItems as IList);

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            Grid.SelectedRowsCollectionChanged += new System.EventHandler<SelectionCollectionChangedEventArgs<SelectedRowsCollection>>(Grid_SelectedRowsCollectionChanged);
        }

        private void UnsubscribeFromEvents()
        {
            Grid.SelectedRowsCollectionChanged -= new System.EventHandler<SelectionCollectionChangedEventArgs<SelectedRowsCollection>>(Grid_SelectedRowsCollectionChanged);
        }

        public static void TransferSourceToTarget(IList source, IList target)
        {
            if (source == null || target == null)
                return;

            target.Clear();

            foreach (var r in from Row r in source where r.RowType == RowType.DataRow && r.ParentRow == null select r)
            {
                target.Add(r.Data);
            }
        }
    }
}
