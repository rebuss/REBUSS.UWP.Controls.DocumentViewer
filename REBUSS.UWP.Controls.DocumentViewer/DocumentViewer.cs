using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace REBUSS.UWP.Controls.DocumentViewer
{
    public class DocumentViewer : Control
    {
        public static readonly DependencyProperty SelectedItemTemplateProperty = DependencyProperty.Register(
            "SelectedItemTemplate", typeof(DataTemplate), typeof(DocumentViewer),
            new PropertyMetadata(default(DataTemplate)));

        public static readonly DependencyProperty NextCommandProperty = DependencyProperty.Register(
            "NextCommand", typeof(ICommand), typeof(DocumentViewer), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty PrevCommandProperty = DependencyProperty.Register(
            "PrevCommand", typeof(ICommand), typeof(DocumentViewer), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IList<object>), typeof(DocumentViewer), new PropertyMetadata(default(IList<object>)));

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof(object), typeof(DocumentViewer),
            new PropertyMetadata(default(object), OnSelectedItemChanged));

        public static readonly DependencyProperty CanNextProperty = DependencyProperty.Register(
            "CanNext", typeof(bool), typeof(DocumentViewer), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty CanPrevProperty = DependencyProperty.Register(
            "CanPrev", typeof(bool), typeof(DocumentViewer), new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            "ItemTemplate", typeof(DataTemplate), typeof(DocumentViewer), new PropertyMetadata(default(DataTemplate)));

        private ListView listView;
        private ScrollViewer scrollViewer;

        public DocumentViewer()
        {
            DefaultStyleKey = typeof(DocumentViewer);
            Loaded += OnLoaded;
        }

        public bool CanPrev
        {
            get => (bool) GetValue(CanPrevProperty);
            set => SetValue(CanPrevProperty, value);
        }

        public bool CanNext
        {
            get => (bool) GetValue(CanNextProperty);
            set => SetValue(CanNextProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public IList<object> ItemsSource
        {
            get => (IList<object>) GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate SelectedItemTemplate
        {
            get => (DataTemplate) GetValue(SelectedItemTemplateProperty);
            set => SetValue(SelectedItemTemplateProperty, value);
        }

        public ICommand NextCommand
        {
            get => (ICommand) GetValue(NextCommandProperty);
            set => SetValue(NextCommandProperty, value);
        }

        public ICommand PrevCommand
        {
            get => (ICommand) GetValue(PrevCommandProperty);
            set => SetValue(PrevCommandProperty, value);
        }
        
        public void Next()
        {
            if (listView.Items.Count > listView.SelectedIndex + 1)
            {
                listView.SelectedIndex++;
                // TODO update CanNext and CanPrev
                // TODO bind buttons to CanNext and CanPrev
            }
        }

        public void Prev()
        {
            if (listView.SelectedIndex - 1 >= 0)
            {
                listView.SelectedIndex--;
                // TODO update CanNext and CanPrev
                // TODO bind buttons to CanNext and CanPrev
            }
        }

        private static async void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var docViewer = d as DocumentViewer;
            if (docViewer != null)
            {
                await docViewer.ScrollToItemAsync(e.NewValue);
                docViewer.UpdateListViewSelectedItem();
            }
        }
        
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            listView = GetTemplateChild("ListView") as ListView;
            scrollViewer = listView?.GetChild<ScrollViewer>();
        }

        private void UpdateListViewSelectedItem()
        {
            var index = ItemsSource.IndexOf(SelectedItem);
            if (listView.SelectedIndex != index)
            {
                listView.SelectedIndex = index;
            }
        }
        
        private async Task ScrollToItemAsync(object item)
        {
            var selectorItem = await GetSelectorItemAsync(item);
            await scrollViewer.ScrollToItem(selectorItem);
        }

        private async Task<SelectorItem> GetSelectorItemAsync(object item)
        {
            var selectorItem = listView.ContainerFromItem(item) as SelectorItem;
            // when it's null, it means that virtualization is on and the item hasn't been realized yet
            if (selectorItem == null)
            {
                await scrollViewer.ScrollIntoViewAsync(item, listView);
                selectorItem = (SelectorItem)listView.ContainerFromItem(item);
            }

            return selectorItem;
        }
    }
}