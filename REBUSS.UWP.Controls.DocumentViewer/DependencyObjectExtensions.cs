using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace REBUSS.UWP.Controls.DocumentViewer
{
    public static class DependencyObjectExtensions
    {
        public static T GetChild<T>(this DependencyObject element) where T : DependencyObject
        {
            if (element is T)
            {
                return (T)element;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);
                var result = GetChild<T>(child);
                if (result == null)
                {
                    continue;
                }

                return result;
            }

            return null;
        }
    }
}
