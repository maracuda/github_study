using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using StudyProjectOne.Models;

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
            var lines_to_return = (from line in reader.ReadToEnd().Split('\n').Where(t=>t != "").ToList() let id = line.Split(',')[0] let prefix = line.Split(',')[1] select new RepositoryEntity(){Id = id, Prefix = prefix}).ToList();
            reader.Close();
            return lines_to_return;
        }

        public void Update(ICollection<Prefix> collection_to_update) //Может заменить все 3 на write()?
        {
            var lines_to_write =
                collection_to_update.Aggregate("", (current, t) => current + t.Id + "," + t.ToString() + "\n");
            var writer = new StreamWriter(_path);
            writer.Write(lines_to_write);
            writer.Close();
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