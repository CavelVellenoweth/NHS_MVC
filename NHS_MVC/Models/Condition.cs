using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace NHS_MVC.Models
{
    public class Condition
    {
        public string @Context { get; set; }
        public string @Type { get; set; }
        public string Name { get; set; }
        public CopyrightHolder CopyrightHolder { get; set; }
        public string License { get; set; }
        public Author Author { get; set; }
        public About About { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Breadcrumb Breadcrumb { get; set; }
        public string Genre { get; set; }
        //public List<SignificantLink> SignificantLink { get; set; } search results page only
        //public List<MainEntityOfPage> MainEntityOfPage { get; set; } not in search results
        //public List<RelatedLink> RelatedLink { get; set; } medical + web page
        //public List<HasPart> HasPart { get; set; } Collection Page
        // alternative headline in medicalwebpage
    }   
    

}
