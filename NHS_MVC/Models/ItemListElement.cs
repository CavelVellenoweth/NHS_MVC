using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_MVC.Models
{
    public class ItemListElement
    {
        public string @Type { get; set; }
        public int Position { get; set; }
        public Item Item { get; set; }
    }
}
