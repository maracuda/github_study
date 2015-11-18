using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudyProjectOne.Repository
{
    /// <summary>
    ///     Класс для работы с репозиторием из файла
    /// </summary>
    public class FileRepository : IRepository
    {
        private readonly string _path;

        public FileRepository(string path_strng)
        {
            _path = path_strng;
        }

        public ICollection<RepositoryEntity> Read()
        {
            var reader = new StreamReader(File.OpenRead(_path));
            var lines_to_return = (from line in reader.ReadToEnd().Split('\n').Where(t => t != "").ToList()
                let id = line.Split(',')[0]
                let prefix = line.Split(',')[1]
                select new RepositoryEntity(id, prefix)).ToList();
            reader.Close();
            return lines_to_return;
        }

        public void Update(ICollection<RepositoryEntity> collection_to_update)
            //Может заменить все 3 (Update, Insert, Remove) на write()?
        {
            var lines_to_write =
                collection_to_update.Aggregate("", (current, t) => current + t.Id + "," + t.PrefixString + "\n");
            var writer = new StreamWriter(_path);
            writer.Write(lines_to_write);
            writer.Close();
        }

        public void Insert(ICollection<RepositoryEntity> collection_to_insert)
        {
            Update(collection_to_insert);
        }

        public void Remove(ICollection<RepositoryEntity> collection_to_remove)
        {
            Update(collection_to_remove);
        }
    }
}