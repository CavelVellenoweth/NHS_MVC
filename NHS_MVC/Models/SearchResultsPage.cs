using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_MVC.Models
{
    public class SearchResultsPage : Condition
    {
        public List<SignificantLink> SignificantLink { get; set; }
    }
}
