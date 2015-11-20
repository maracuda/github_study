using System.Collections.Generic;

namespace PrefixLibrary.Repository
{
    /// <summary>
    ///     Интерфейс репозитория
    /// </summary>
    public interface IRepository
    {
        ICollection<RepositoryEntity> Read();
        void Update(ICollection<RepositoryEntity> collection_to_update);
        void Insert(ICollection<RepositoryEntity> collection_to_insert);
        void Remove(ICollection<RepositoryEntity> collection_to_remove);
    }
}