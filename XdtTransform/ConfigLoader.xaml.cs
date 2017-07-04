using System.Windows;
using XdtTransform.Messages;
using XdtTransform.ViewModel;

namespace XdtTransform
{
    /// <summary>
    ///     Interaction logic for ConfigLoader.xaml
    /// </summary>
    public partial class ConfigLoader
    {
        public ConfigLoader()
        {
            InitializeComponent();
        }

        public FileType FileType
        {
            get => (FileType) GetValue(FileTypeProperty);
            set => SetValue(FileTypeProperty, value);
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty FileTypeProperty =
            DependencyProperty.Register(
                "FileType", 
                typeof(FileType),
                typeof(ConfigLoader), 
                new PropertyMetadata(FileType.Unknown, OnFileTypePropertyChanged));

        private static void OnFileTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var source = (ConfigLoader) d;
            var viewmodel = source.DataContext as ConfigLoaderVm;
            source.OnFileTypePropertyChanged(viewmodel);
        }

        private void OnFileTypePropertyChanged(ConfigLoaderVm viewmodel)
        {
            if (viewmodel == null)
                return;

            viewmodel.Type = FileType;
        }
    }
}