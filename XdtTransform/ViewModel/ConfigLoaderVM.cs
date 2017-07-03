using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using XdtTransform.Messages;

namespace XdtTransform.ViewModel
{
    public class ConfigLoaderVM : ViewModelBase
    {
        private string _filePath;

        public ConfigLoaderVM()
        {
            OpenFile = new RelayCommand(Open);
        }

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public RelayCommand OpenFile { get; }

        public string FilePath
        {
            get => _filePath;
            set => Set(ref _filePath, value, true);
        }

        private void Open()
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Config file (.config)|*.config|XML File (.xml)|*.xml"
            };
            if (dialog.ShowDialog() != true) return;

            FilePath = dialog.FileName;
            Messenger.Default.Send(new FileOpened
            {
                FilePath = FilePath
            });
        }
    }
}