namespace PrefixLibrary.Repository
{
    public interface IPrefixValidator
    {
        string IsPrefixStringUnique(string prefix_string);
        string IsPrefixIdUnique(string id);
    }
}