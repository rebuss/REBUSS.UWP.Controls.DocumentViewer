using Windows.UI.Xaml.Media.Imaging;

namespace REBUSS.UWP.Controls.DocumentViewerDemo
{
    class Document : IDocument
    {
        public bool IsProcessed { get; set; }
        public BitmapImage ImageBitmap { get; set; }
    }
}
