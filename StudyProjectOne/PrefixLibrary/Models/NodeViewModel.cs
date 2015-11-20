using System.Collections.Generic;

namespace PrefixLibrary.Models
{
    /// <summary>
    ///     Модель вершины покрывающего набора для передачи в представление
    /// </summary>
    public class NodeViewModel
    {
        public PrefixViewModel Prefix { get; set; }
        public List<PrefixViewModel> ChildList { get; set; }
    }
}