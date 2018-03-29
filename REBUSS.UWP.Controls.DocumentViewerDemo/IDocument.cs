using System;
using Windows.UI.Xaml.Media.Imaging;

namespace REBUSS.UWP.Controls.DocumentViewerDemo
{
    public interface IDocument
    {
        bool IsProcessed { get; set; }

        BitmapImage ImageBitmap { get; set; }
    }
}
