using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
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
            RaisePropertyChanged(() => Document);
            Messenger.Default.Send(new FileOpened
            {
                FilePath = FilePath
            });
        }

        private readonly FlowDocument _flowDocument = new FlowDocument();
        public FlowDocument Document
        {
            get
            {
                if (File.Exists(FilePath))
                {
                    var range = new TextRange(_flowDocument.ContentStart, _flowDocument.ContentEnd);
                    using (var stream = new FileStream(FilePath, FileMode.Open))
                    {
                        range.Load(stream, DataFormats.Text);
                    }
                }

                return _flowDocument;
            }
        }
    }
}