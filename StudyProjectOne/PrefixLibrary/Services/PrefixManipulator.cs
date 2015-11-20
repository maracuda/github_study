using System.Collections.Generic;
using System.Linq;
using PrefixLibrary.Models;
using PrefixLibrary.Repository;

namespace PrefixLibrary.Services
{
    /// <summary>
    ///     Основная логика проекта
    /// </summary>
    public class PrefixManipulator
    {
        private readonly IRepository _repository;
        private List<RepositoryEntity> _prefixRepositoryList;
        private List<PrefixViewModel> _prefixViewList;

        /// <summary>
        ///     Конструктор класса
        /// </summary>
        /// <param name="repository">Экземпляр репозитория</param>
        public PrefixManipulator(IRepository repository)
        {
            _repository = repository;
            PrefixList = _repository.Read().Select(t => new Prefix(t.Id, t.PrefixString)).ToList();
        }

        /// <summary>
        ///     Список префиксов для передачи в представление
        /// </summary>
        public List<PrefixViewModel> PrefixViewList
        {
            get
            {
                _prefixViewList = PrefixList.Select(t => new PrefixViewModel(t.Id, t.PrefixString)).ToList();
                return _prefixViewList;
            }
        }

        /// <summary>
        ///     Список префиксов для передачи в репозиторий
        /// </summary>
        public List<RepositoryEntity> PrefixRepositoryList
        {
            get
            {
                _prefixRepositoryList = PrefixList.Select(t => new RepositoryEntity(t.Id, t.PrefixString)).ToList();
                return _prefixRepositoryList;
            }
        }

        /// <summary>
        ///     Список префиксов для внутреннего использования
        /// </summary>
        private List<Prefix> PrefixList { get; set; }

        /// <summary>
        ///     Удаление префикса по Id
        /// </summary>
        /// <param name="id">Строковое представление Id</param>
        public void RemovePrefix(string id)
        {
            PrefixList.Remove(PrefixList.Last(t => t.Id == id));
            _repository.Remove(PrefixRepositoryList);
        }

        /// <summary>
        ///     Добавление префикса
        /// </summary>
        /// <param name="prefix">Экземпляр модели представления</param>
        public void AddPrefix(PrefixViewModel prefix)
        {
            PrefixList.Add(new Prefix(prefix.Id, prefix.PrefixString));
            _repository.Insert(PrefixRepositoryList);
        }

        /// <summary>
        ///     Изменение префикса
        /// </summary>
        /// <param name="old_prefix">Старый префиск</param>
        /// <param name="new_prefix">Новый префиск</param>
        public void UpdatePrefix(PrefixViewModel old_prefix, PrefixViewModel new_prefix)
        {
            PrefixList.RemoveAll(x => x.Id == old_prefix.Id);
            PrefixList.Add(new Prefix(new_prefix.Id, new_prefix.PrefixString));
            _repository.Update(PrefixRepositoryList);
        }

        //Можно построить полное дерево из PrefixNode, спускаясь по значениям словаря 
        /// <summary>
        ///     Метод построения минимального покрывающего набора
        /// </summary>
        /// <returns>Список префиксов и их потомков</returns>
        public List<NodeViewModel> SpanningList()
        {
            var sorted_prefix_list = PrefixList;
            sorted_prefix_list.Sort(new Prefix.PrefixComparer());
            var out_prefix_list = new PrefixNode(new Prefix("0", "0.0.0.0/0"));
            var i = 0;

            while (i < sorted_prefix_list.Count - 1)
            {
                var current_prefix = sorted_prefix_list[i];
                var next_prefix = sorted_prefix_list[i + 1];
                out_prefix_list.Childs.Add(current_prefix.UniqueNetworkId, new PrefixNode(current_prefix));

                if (current_prefix.Network + (2 << 32 - current_prefix.PrefixLength - 1) >
                    next_prefix.Network)
                {
                    while (i < sorted_prefix_list.Count - 1 && current_prefix.Network +
                           (2 << 32 - current_prefix.PrefixLength - 1) > sorted_prefix_list[i + 1].Network)
                    {
                        out_prefix_list.Childs[current_prefix.UniqueNetworkId].Childs.Add(
                            sorted_prefix_list[i + 1].UniqueNetworkId,
                            new PrefixNode(sorted_prefix_list[i + 1]));
                        i++;
                    }
                }
                i++;
            }
            //Добавить единственный первый или последний префикс без родителя
            if (i == sorted_prefix_list.Count - 1)
            {
                out_prefix_list.Childs.Add(sorted_prefix_list[i].UniqueNetworkId, new PrefixNode(sorted_prefix_list[i]));
            }

            var list_out = out_prefix_list.Childs.Values.Select(node =>
                new NodeViewModel
                {
                    Prefix = new PrefixViewModel(node.Node.Id, node.Node.PrefixString),
                    ChildList =
                        node.Childs.Select(t => new PrefixViewModel(t.Value.Node.Id, t.Value.Node.PrefixString))
                            .ToList()
                })
                .ToList();

            return list_out;
        }
    }
}