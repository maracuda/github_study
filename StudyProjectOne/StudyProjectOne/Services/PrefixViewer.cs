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
        public PrefixViewer(IRepository repository)
        {
            _repository = repository;
        }

        public List<Prefix> GetPrefixies()
        {
            return (from RepositoryEntity prefix in _repository.Read() select new Prefix(prefix.Id, prefix.Prefix)).ToList();
        }
    }
}