﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using StudyProjectOne.Models;
using StudyProjectOne.Repository;

namespace StudyProjectOne.Services
{
   
    internal class PrefixManipulator
    {
        private readonly IRepository _repository;
        private List<RepositoryEntity> _prefixRepositoryList;
        private List<PrefixViewModel> _prefixViewList;

        public List<PrefixViewModel> PrefixViewList
        {
            get
            {
                _prefixViewList = PrefixList.Select(t => new PrefixViewModel(t.Id, t.PrefixString)).ToList();
                return _prefixViewList;
            }
        }
        public List<RepositoryEntity> PrefixRepositoryList
        {
            get
            {
                _prefixRepositoryList = PrefixList.Select(t => new RepositoryEntity(t.Id, t.PrefixString)).ToList();
                return _prefixRepositoryList;
            }
        }
        public List<Prefix> PrefixList { get; private set; }

        public PrefixManipulator(IRepository repository)
        {
            _repository = repository;
            PrefixList = _repository.Read().Select(t => new Prefix(t.Id, t.PrefixString)).ToList();
        }

        public void RemovePrefix(string id)
        {
            PrefixList.Remove(PrefixList.Last(t => t.Id == id));
            _repository.Remove(PrefixRepositoryList);
        }

        public void AddPrefix(PrefixViewModel prefix)
        {
            PrefixList.Add(new Prefix(prefix.Id, prefix.PrefixString));
            _repository.Insert(PrefixRepositoryList);
        }

        public void UpdatePrefix(PrefixViewModel old_prefix, PrefixViewModel new_prefix)
        {
            PrefixList.RemoveAll(x => x.Id == old_prefix.Id);
            PrefixList.Add(new Prefix(new_prefix.Id, new_prefix.PrefixString));
            _repository.Update(PrefixRepositoryList);
        }

        //Можно построить полное дерево из PrefixNode, спускаясь по значениям словаря 
        public List<NodeViewModel> SpanningList()
        {
            var sorted_prefix_list = PrefixList;
            sorted_prefix_list.Sort(new PrefixComparer());
            var out_prefix_list = new PrefixNode(new Prefix("0", "0.0.0.0/0"));
            var i = 0;

            while (i < sorted_prefix_list.Count - 1)
            {
                var current_prefix = sorted_prefix_list[i];
                var next_prefix = sorted_prefix_list[i + 1];
                out_prefix_list.Childs.Add(current_prefix.UniqueNetworkId, new PrefixNode(current_prefix));

                if (current_prefix.Network + (int) (Math.Pow(2, 32 - current_prefix.PrefixLength) - 1) >
                    next_prefix.Network)
                {
                    while (i < sorted_prefix_list.Count - 1 && current_prefix.Network +
                        (int)(Math.Pow(2, 32 - current_prefix.PrefixLength) - 1) > sorted_prefix_list[i + 1].Network)
                    {
                        out_prefix_list.Childs[current_prefix.UniqueNetworkId].Childs.Add(sorted_prefix_list[i + 1].UniqueNetworkId,
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
                new NodeViewModel{Prefix = new PrefixViewModel(node.Node.Id, node.Node.PrefixString),
                    ChildList = node.Childs.Select(t => new PrefixViewModel(t.Value.Node.Id, t.Value.Node.PrefixString)).ToList()})
                        .ToList();

            return list_out;
        }

        class PrefixComparer : Comparer<Prefix>
        {
            public override int Compare(Prefix x, Prefix y)
            {
                if (object.Equals(x, y)) return 0;
                return x.Network == y.Network ? x.PrefixLength.CompareTo(y.PrefixLength) : x.Network.CompareTo(y.Network);
            }
        }
    }
}