using System.Text.RegularExpressions;

namespace StraregyBracketsLibrary.Tests
{
    public class Parser : IParser
    {
        private Regex regex = new Regex("(ИП )?(?<fio>.*)");

        public bool TryParse(string source, out string fio)
        {
            var match = regex.Match(source);
            fio = match.Success ? match.Groups["fio"].Value : null;
            return match.Success;
        }
    }
}