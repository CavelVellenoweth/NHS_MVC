using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_MVC.Models
{

    public class RelatedLink
    {
        public string Type { get; set; }
        public string Url { get; set; }
        public string LinkRelationship { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
    }
}
