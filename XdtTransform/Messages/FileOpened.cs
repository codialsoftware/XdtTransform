namespace XdtTransform.Messages
{
    public class FileOpened
    {
        public string FilePath { get; set; }
        public FileType Type { get; set; }
    }
}