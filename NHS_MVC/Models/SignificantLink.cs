using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_MVC.Models
{
    public class SignificantLink
    {
        public string Description { get; set; }
        public string LinkRelationship { get; set; }
        public string @Type { get; set; }
        public string ArticleStatus { get; set; }
        public Page MainEntityOfPage { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
    }
}
