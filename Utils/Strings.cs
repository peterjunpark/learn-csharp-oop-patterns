using System.Text.Json;

namespace Peter.CookiesCookbook.Utils
{
    public interface IStringsRepository
    {
        List<string> Read(string filePath);
        void Write(string filePath, List<string> strings);
    }

    public abstract class StringsRepository : IStringsRepository
    {
        public List<string> Read(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return [];
            }
            string fileContents = File.ReadAllText(filePath);
            return TextToStrings(fileContents);
        }

        public void Write(string filePath, List<string> strings)
        {
            File.WriteAllText(filePath, StringsToText(strings));
        }

        protected abstract List<string> TextToStrings(string fileContents);
        protected abstract string StringsToText(List<string> strings);
    }

    public class StringsTextRepository : StringsRepository
    {
        private static readonly string Separator = Environment.NewLine;

        protected override List<string> TextToStrings(string fileContents)
        {
            return [.. fileContents.Split(Separator)];
        }

        protected override string StringsToText(List<string> strings)
        {
            return string.Join(Separator, strings);
        }
    }

    public class StringsJsonRepository : StringsRepository
    {
        protected override List<string> TextToStrings(string fileContents)
        {
            return JsonSerializer.Deserialize<List<string>>(fileContents);
        }

        protected override string StringsToText(List<string> strings)
        {
            return JsonSerializer.Serialize(strings);
        }
    }
}
