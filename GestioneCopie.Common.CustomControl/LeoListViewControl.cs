using System.Windows;
using System.Windows.Controls;

namespace GestioneCopie.Common.CustomControl
{
    public class LeoListViewControl : ListBox
    {
        static LeoListViewControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LeoListViewControl), new FrameworkPropertyMetadata(typeof(LeoListViewControl)));
        }

        public static readonly DependencyProperty ContentProperty = 
            DependencyProperty.Register("Content", typeof(object), typeof(LeoListViewControl), new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsRender |
                FrameworkPropertyMetadataOptions.AffectsParentMeasure));

            /// <summary>
            /// Gets or sets the Content.
            /// </summary>
            /// <value>The Content.</value>
            public object Content
            {
                get { return (object)GetValue(ContentProperty); }
                set { SetValue(ContentProperty, value); }
            }
    }
}