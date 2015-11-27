using System.Collections.Generic;
using PrefixLibrary.Repository;

namespace PrefixLibrary.Models
{
    /// <summary>
    ///     Модель вершины покрывающего набора для передачи в представление
    /// </summary>
    public class NodeViewModel
    {
        /// <summary>
        /// Вершина дерева
        /// </summary>
        public PrefixView Prefix { get; set; }
        /// <summary>
        /// Список потомков
        /// </summary>
        public List<PrefixView> ChildList { get; set; }
    }
}