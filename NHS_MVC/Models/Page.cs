using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_MVC.Models
{
    public class Page
    {
        public string @Type { get; set; }
        public string Genre { get; set; }
        public string DatePublished { get; set; }
        public string DateModified { get; set; }
        public List<string> LastReviewed { get; set; }
        public string ReviewDue { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> AlternateName { get; set; } 
        public List<PageCode> Code { get; set; }
    }
}
