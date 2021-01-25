using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_MVC.Models
{
    public class CollectionPage : Condition
    {
        public List<MainEntityOfPage> MainEntityOfPage { get; set; }
        public List<RelatedLink> RelatedLink { get; set; }
        public List<HasPart> HasPart { get; set; }
    }
}
