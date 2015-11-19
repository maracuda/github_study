using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyProjectOne.Models
{

    public class NodeViewModel
    {
        public PrefixViewModel Prefix { get; set; }
        public List<PrefixViewModel> ChildList { get; set; } 

        public NodeViewModel()
        {
        }

    }
}