namespace StraregyBracketsLibrary.Tests
{
    public interface IParser
    {
        bool TryParse(string source, out string fio);
    }
}