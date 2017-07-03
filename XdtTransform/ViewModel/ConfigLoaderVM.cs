using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using XdtTransform.Messages;

namespace XdtTransform.ViewModel
{
    public class ConfigLoaderVM : ViewModelBase
    {
        private string _filePath;

        public ConfigLoaderVM()
        {
            OpenFile = new RelayCommand<string>(Open);
        }

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public RelayCommand<string> OpenFile { get; }

        public string FilePath
        {
            get => _filePath;
            set => Set(ref _filePath, value, true);
        }

        private void Open(string path)
        {
            if (File.Exists(path))
            {
                FilePath = path;
                Messenger.Default.Send(new FileOpened
                {
                    FilePath = FilePath
                });
            }
        }
    }
}