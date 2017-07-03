using System.IO;
using System.Windows;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using XdtTransform.Messages;

namespace XdtTransform.ViewModel
{
    public class ConfigLoaderVm : ViewModelBase
    {
        private readonly FlowDocument _flowDocument = new FlowDocument();
        private string _filePath;

        public ConfigLoaderVm()
        {
            OpenFile = new RelayCommand(Open);
        }

        public RelayCommand OpenFile { get; }

        public string FilePath
        {
            get => _filePath;
            private set => Set(ref _filePath, value, true);
        }

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
    }
}