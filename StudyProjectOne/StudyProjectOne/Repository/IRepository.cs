using System.Collections.Generic;

namespace StudyProjectOne.Repository
{
    /// <summary>
    ///     Интерфейс репозитория
    /// </summary>
    internal interface IRepository
    {
        ICollection<RepositoryEntity> Read();
        void Update(ICollection<RepositoryEntity> collection_to_update);
        void Insert(ICollection<RepositoryEntity> collection_to_insert);
        void Remove(ICollection<RepositoryEntity> collection_to_remove);
    }
}