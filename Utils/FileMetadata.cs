namespace Peter.CookiesCookbook.Utils
{
    public enum FileFormat
    {
        Json,
        Txt
    }

    public class FileMetadata(string name, FileFormat format)
    {
        public string Name { get; } = name;
        public FileFormat Format { get; } = format;

        public string ToPath()
        {
            return Name + Format.AsFileExtension();
        }
    }

    public static class FileFormatExtensions
    {
        public static string AsFileExtension(this FileFormat fileFormat)
        {
            return fileFormat == FileFormat.Json ? ".json" : ".txt";
        }
    }
}
