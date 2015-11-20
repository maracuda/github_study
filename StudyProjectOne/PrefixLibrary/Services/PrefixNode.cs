using System.Collections.Generic;

namespace PrefixLibrary.Services
{
    /// <summary>
    ///     Описывает вершину в минимальном покрывающем наборе
    /// </summary>
    public class PrefixNode
    {
        public PrefixNode(Prefix node)
        {
            Node = node;
            Childs = new SortedList<long, PrefixNode>();
        }

        /// <summary>
        ///     Вершина - префикс
        /// </summary>
        public Prefix Node { get; set; }

        /// <summary>
        ///     Список потомков, отсортированный список для возможного построения полного дерева
        /// </summary>
        public SortedList<long, PrefixNode> Childs { get; set; }
    }
}