using System.Collections.Generic;
using System.Linq;
using PrefixLibrary.Models;
using PrefixLibrary.Repository;

namespace PrefixLibrary.Services
{
    public class SpanningTreeBuilder
    {
        private readonly List<EditablePrefix> _prefixList;

        public SpanningTreeBuilder(PrefixView[] entities)
        {
            _prefixList = entities.Select(t => new EditablePrefix(t.Id, t.PrefixString)).ToList();
        }

        public List<NodeViewModel> SpanningList()
        {
            var sorted_prefix_list = _prefixList;
            sorted_prefix_list.Sort(new EditablePrefix.PrefixComparer());
            var out_prefix_list = new PrefixNode(new EditablePrefix("0", "0.0.0.0/0"));
            var i = 0;

            while (i < sorted_prefix_list.Count - 1)
            {
                var current_prefix = sorted_prefix_list[i];
                var next_prefix = sorted_prefix_list[i + 1];
                out_prefix_list.Childs.Add(current_prefix.UniqueNetworkId, new PrefixNode(current_prefix));

                if (current_prefix.Network + ((1 << 32 - current_prefix.PrefixLength) - 1) >
                    next_prefix.Network)
                {
                    while (i < sorted_prefix_list.Count - 1 && current_prefix.Network +
                           ((1 << 32 - current_prefix.PrefixLength) - 1) > sorted_prefix_list[i + 1].Network)
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
                    Prefix = new PrefixView(node.Node.Id, node.Node.PrefixString),
                    ChildList =
                        node.Childs.Select(t => new PrefixView(t.Value.Node.Id, t.Value.Node.PrefixString))
                            .ToList()
                })
                .ToList();

            return list_out;
        }
    }
}