using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudyProjectOne.Models;
using StudyProjectOne.Repository;

namespace StudyProjectOne.Services
{
    /// <summary>
    /// Метод отображения таблицы с префиксами
    /// </summary>
    class PrefixViewer
    {
        private readonly IRepository _repository;
        private List<Prefix> _repoPrefixes; 
        public PrefixViewer(IRepository repository)
        {
            _repository = repository;
        }

        public List<Prefix> GetPrefixies()
        {
            _repoPrefixes =
                (from RepositoryEntity prefix in _repository.Read() select new Prefix(prefix.Id, prefix.Network)).ToList();
            return _repoPrefixes;
        }

        public void RemovePrefix(string id)
        {
            _repoPrefixes = GetPrefixies();
            _repoPrefixes.Remove(_repoPrefixes.Last(t => t.Id == id));
            _repository.Update(_repoPrefixes);
        }

        public void AddPrefix(Prefix prefix)
        {
            _repoPrefixes = GetPrefixies();
            _repoPrefixes.Add(prefix);
            _repository.Update(_repoPrefixes);
        }

        public void UpdatePrefix(Prefix old_prefix, Prefix new_prefix)
        {
            _repoPrefixes = GetPrefixies();
            _repoPrefixes.RemoveAll(x => x.Id == old_prefix.Id);
            _repoPrefixes.Add(new_prefix);
            _repository.Update(_repoPrefixes);
        }
 
    }
}