using System.Collections.Generic;

namespace PrefixLibrary.Repository
{
    /// <summary>
    ///     Интерфейс репозитория
    /// </summary>
    public interface IRepository<T>
        where T : class
    {
        // получение всех объектов
        IEnumerable<T> GetPrefixList();
        // получение одного объекта по id
        T GetPrefix(string id);
        // создание объекта
        void Create(T item);
        // обновление объекта
        void Update(T item_old, T item_new);
        // удаление объекта по id
        void Delete(string id);
        // сохранение изменений
        void Save();  
    }
}