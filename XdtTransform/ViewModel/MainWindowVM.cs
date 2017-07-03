using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using XdtTransform.Messages;

namespace XdtTransform.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainWindowVM : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public RelayCommand<string> OpenFile { get; private set; }

        public MainWindowVM()
        {
            OpenFile = new RelayCommand<string>(Open);
        }

        public string FilePath { get; private set; }

        void Open(string path)
        {
            if (System.IO.File.Exists(path))
            {
                FilePath = path;
                Messenger.Default.Send<FileOpened>(new FileOpened
                {
                    FilePath = FilePath
                });
            }
        }
    }
}