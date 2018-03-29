using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace REBUSS.UWP.Controls.DocumentViewer
{
    public static class ScrollViewerExtensions
    {
        public static async Task ScrollToItem(this ScrollViewer scrollViewer, SelectorItem selectorItem)
        {
            // calculate the position object in order to know how much to scroll to
            var transform = selectorItem.TransformToVisual((UIElement)scrollViewer.Content);
            var position = transform.TransformPoint(new Point(-scrollViewer.ViewportWidth / 2.0, 0));

            // touch selection works without animation so the workaround is needed
            await Task.Delay(1);
            scrollViewer.ChangeView(position.X, position.Y, null);
        }

        public static async Task ScrollIntoViewAsync(this ScrollViewer scrollViewer, object item, ListView listView)
        {
            var taskCompletionSource = new TaskCompletionSource<object>();

            void ViewChanged(object source, ScrollViewerViewChangedEventArgs eventArgs) => taskCompletionSource.TrySetResult(null);
            try
            {
                scrollViewer.ViewChanged += ViewChanged;
                listView.ScrollIntoView(item, ScrollIntoViewAlignment.Leading);
                await taskCompletionSource.Task;
            }
            finally
            {
                scrollViewer.ViewChanged -= ViewChanged;
            }
        }
    }
}
