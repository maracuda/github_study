using System.Collections.Generic;
using System.Linq;
using PrefixLibrary.Models;
using PrefixLibrary.Repository;

namespace PrefixLibrary.Services
{
    public static class UniqueValidator
    {
        public static string IsPrefixStringUnique(string prefix_string, PrefixView[] repository_entities)
        {
            var prefix_string_set = new HashSet<string>(repository_entities.Select(t => t.PrefixString));
            return prefix_string_set.Contains(prefix_string) ? "Такой префикс существует!" : "";
        }

        public static string IsPrefixIdUnique(string id, PrefixView[] repository_entities)
        {
            var prefix_id_set_set = new HashSet<string>(repository_entities.Select(t => t.Id));
            return prefix_id_set_set.Contains(id) ? "Префикс с таким id существует!" : "";
        }
    }
}