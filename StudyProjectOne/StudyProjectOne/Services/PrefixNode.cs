using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace StudyProjectOne.Services
{
    public class PrefixNode
    {
        public Prefix Node { get; set; }
        public SortedList<long, PrefixNode> Childs { get; set; }

        public PrefixNode(Prefix node)
        {
            Node = node;
            Childs = new SortedList<long, PrefixNode>();
        }
    }
}