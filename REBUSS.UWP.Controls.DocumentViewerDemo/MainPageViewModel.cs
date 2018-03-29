using REBUSS.UWP.Controls.DocumentViewerDemo.Annotations;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media.Imaging;

namespace REBUSS.UWP.Controls.DocumentViewerDemo
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        private Windows.Storage.StorageFolder folder = null;
        private IDocument _selectedDocument;

        public MainPageViewModel()
        {
            Documents = new ObservableCollection<object>();
        }
        
        public ObservableCollection<object> Documents { get; set; }

        public IDocument SelectedDocument
        {
            get => _selectedDocument;
            set
            {
                _selectedDocument = value;
                OnPropertyChanged();
            }
        }
        
        public async void OpenFolder()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            folderPicker.FileTypeFilter.Add("*");

            folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                // Application now has read/write access to all contents in the picked folder
                // (including other sub-folder contents)
                Windows.Storage.AccessCache.StorageApplicationPermissions.
                    FutureAccessList.AddOrReplace(Consts.Images, folder);
                
                foreach (var file in await folder.GetFilesAsync())
                {
                    if (file.ContentType == "image/png" || file.ContentType == "image/jpeg")
                    {
                        // TODO consider using thumbnail
                        // var thumbnail = await file.GetThumbnailAsync(ThumbnailMode.ListView);
                        var stream = await file.OpenReadAsync();
                        var bitmap = new BitmapImage { DecodePixelType = DecodePixelType.Logical };
                        bitmap.SetSource(stream);
                        Documents.Add(new Document { ImageBitmap = bitmap });
                    }
                }
                
                OnPropertyChanged(nameof(Documents));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
