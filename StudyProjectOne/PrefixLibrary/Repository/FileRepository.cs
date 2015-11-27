using System.Collections.Generic;
using System.IO;
using System.Linq;
using PrefixLibrary.Models;

namespace PrefixLibrary.Repository
{
    public class FileRepository : IRepository<PrefixView>, IPrefixValidator
    {
        private readonly string _path;
        private Dictionary<string, PrefixView> _repositoryCache;

        public FileRepository(string path)
        {
            _path = path;
        }

        private HashSet<string> RepositoryValuesSet
        {
            get { return new HashSet<string>(RepositoryCache.Values.Select(t => t.PrefixString)); }
        }

        private Dictionary<string, PrefixView> RepositoryCache
        {
            get
            {
                return _repositoryCache ?? (_repositoryCache = File.ReadLines(_path)
                    .Select(t => new PrefixView(t.Split(',')[0], t.Split(',')[1]))
                    .ToDictionary(t => t.Id, t => t));
            }
        }

        public string IsPrefixStringUnique(string prefix_string)
        {
            return RepositoryValuesSet.Contains(prefix_string) ? "Такой префикс существует!" : "";
        }

        public string IsPrefixIdUnique(string id)
        {
            //Странности, если заменить RepositoryCache на _repositoryCache, то _repositoryCache = Null, но ведь
            //не должен. Уже была операция чтения. А если подождать 10 секунд, то становится not null
            return RepositoryCache.ContainsKey(id) ? "Префикс с таким Id существует!" : "";
        }

        public void Add(PrefixView entity)
        {
            RepositoryCache.Add(entity.Id, entity);
            //Как по-человечески записать одну строку? подумать...
            File.AppendAllLines(_path, new[] {entity.ToString()});
        }

        public void Delete(string id)
        {
            RepositoryCache.Remove(id);
            SaveCache();
        }

        public void Update(string id, PrefixView entity)
        {
            RepositoryCache[id] = entity;
            SaveCache();
        }

        public PrefixView[] ReadAll()
        {
            return RepositoryCache.Values.ToArray();
        }

        public PrefixView Read(string id)
        {
            return RepositoryCache[id];
        }

        private void SaveCache()
        {
            File.WriteAllLines(_path, RepositoryCache.Values.Select(t => t.ToString()));
        }
    }
}