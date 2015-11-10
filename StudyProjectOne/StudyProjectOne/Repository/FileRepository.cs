using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudyProjectOne.Repository
{
    /// <summary>
    /// Класс для работы с репозиторием из файла
    /// </summary>
    public class FileRepository : IRepository
    {
        private string _path; 
        public FileRepository(string path_strng)
        {
            _path = path_strng;
        }
        public IEnumerable Read()
        {
            var reader = new StreamReader(File.OpenRead(_path));
            var lines_to_return = (from line in reader.ReadToEnd().Split('\n').ToList() let id = line.Split(',')[0] let prefix = line.Split(',')[1] select new RepositoryEntity(){Id = id, Prefix = prefix}).ToList();
            return lines_to_return;
        }

        public void Update(IEnumerable collection_to_update)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEnumerable collection_to_insert)
        {
            throw new NotImplementedException();
        }

        public void Remove(IEnumerable collection_to_remove)
        {
            throw new NotImplementedException();
        }
    }
}