using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefixLibrary.Repository
{
    public class FileRepository : IRepository<RepositoryEntity>
    {
        private readonly string _path;
        private IEnumerable<RepositoryEntity> _inputEnumerable;

        public FileRepository(string path)
        {
            _path = path;
        }

        public IEnumerable<RepositoryEntity> GetPrefixList()
        {
            var reader = new StreamReader(File.OpenRead(_path));
            _inputEnumerable = (from line in reader.ReadToEnd().Split('\n').Where(t => t != "")
                                   let id = line.Split(',')[0]
                                   let prefix = line.Split(',')[1]
                                   select new RepositoryEntity(id, prefix));
            reader.Close();
            return _inputEnumerable;
        }

        public RepositoryEntity GetPrefix(string id)
        {
            return _inputEnumerable.First(t => t.Id == id);
        }

        public void Create(RepositoryEntity item)
        {
            var buffer_list = _inputEnumerable.ToList();
            buffer_list.Add(item);
            _inputEnumerable = buffer_list.AsEnumerable();
            Write(_inputEnumerable);
        }

        public void Update(RepositoryEntity item_old, RepositoryEntity item_new)
        {
            var buffer_list = _inputEnumerable.ToList();
            buffer_list.RemoveAll(t => t.Id == item_old.Id);
            buffer_list.Add(item_new);
            _inputEnumerable = buffer_list.AsEnumerable();
            Write(_inputEnumerable);
        }

        public void Delete(string id)
        {
            var buffer_list = _inputEnumerable.ToList();
            buffer_list.RemoveAll(t => t.Id == id);
            _inputEnumerable = buffer_list.AsEnumerable();
            Write(_inputEnumerable);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        private void Write(IEnumerable<RepositoryEntity> enumerable_to_write)
        {
            var lines_to_write =
                enumerable_to_write.Aggregate("", (current, t) => current + t.Id + "," + t.PrefixString + "\n");
            var writer = new StreamWriter(_path);
            writer.Write(lines_to_write);
            writer.Close();
        }
    }
}
