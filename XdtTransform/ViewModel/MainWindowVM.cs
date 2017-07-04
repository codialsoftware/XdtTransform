using System;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using XdtTransform.Messages;
using System.IO;
using System.Windows;
using Microsoft.Web.XmlTransform;

namespace XdtTransform.ViewModel
{
    public class MainWindowVm : ViewModelBase
    {
        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainWindowVm()
        {
            MessengerInstance.Register<FileOpened>(this, true, OnFileOpened);
        }

        public FlowDocument Document { get; } = new FlowDocument();

        private string _sourceFilePath;
        private string _transformationFilePath;

        private void OnFileOpened(FileOpened message)
        {
            StoreFilePath(value => _sourceFilePath = value, message, FileType.Source);
            StoreFilePath(value => _transformationFilePath = value, message, FileType.Transformation);
            TransformFile();
        }

        private void TransformFile()
        {
            if (!File.Exists(_sourceFilePath) || !File.Exists(_transformationFilePath))
                return;

            using (var source = new XmlTransformableDocument())
            using (var transformation = new XmlTransformation(_transformationFilePath))
            {
                source.PreserveWhitespace = true;
                source.Load(_sourceFilePath);
                DisplayDocument(TransformFile(source, transformation));
            }
        }

        private void DisplayDocument(Stream content)
        {
            if (content == null) return;

            var range = new TextRange(Document.ContentStart, Document.ContentEnd);
            range.Load(content, DataFormats.Text);
        }

        private Stream TransformFile(XmlTransformableDocument sourceFilePath, XmlTransformation transformationFilePath)
        {
            if (transformationFilePath.Apply(sourceFilePath))
            {
                var result = new MemoryStream();
                sourceFilePath.Save(result);
                return result;
            }

            return null;
        }

        private void StoreFilePath(Action<string> actionWhenMessageOfType, FileOpened message, FileType propertyType)
        {
            if (message.Type != propertyType)
                return;

            actionWhenMessageOfType(message.FilePath);
        }
    }
}