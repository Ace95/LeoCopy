using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Interactivity;

using Infragistics.Controls.Editors;

namespace GestioneCopie.Common.Resources.Behaviors
{
    public class XamComboEditorSelectedItemsBehavior : Behavior<XamComboEditor>
    {
        private XamComboEditor Combobox
        {
            get { return AssociatedObject as XamComboEditor; }
        }

        public INotifyCollectionChanged SelectedItems
        {
            get { return (INotifyCollectionChanged)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems",
            typeof(INotifyCollectionChanged),
            typeof(XamComboEditorSelectedItemsBehavior),
            null);

        protected override void OnAttached()
        {
            base.OnAttached();
            SubscribeToEvents();
        }

        void ContextSelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();
            Transfer(SelectedItems as IList, Combobox.SelectedItems);
            SubscribeToEvents();
        }

        void SelectedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();
            Transfer(Combobox.SelectedItems, SelectedItems as IList);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            Combobox.SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;

            if (SelectedItems != null)
            {
                SelectedItems.CollectionChanged += ContextSelectedItems_CollectionChanged;
            }
        }

        private void UnsubscribeFromEvents()
        {
            Combobox.SelectedItems.CollectionChanged -= SelectedItems_CollectionChanged;

            if (SelectedItems != null)
            {
                SelectedItems.CollectionChanged -= ContextSelectedItems_CollectionChanged;
            }
        }

        public static void Transfer(IList source, IList target)
        {
            if (source == null || target == null) { return; }

            target.Clear();

            foreach (var o in source)
            {
                target.Add(o);
            }
        }
    }
}
