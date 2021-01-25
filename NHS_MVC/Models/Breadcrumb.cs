using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_MVC.Models
{

    public class Breadcrumb
    {
        public string @Context { get; set; }
        public string @Type { get; set; }
        public List<ItemListElement> ItemListElement { get; set; }
    }
}
