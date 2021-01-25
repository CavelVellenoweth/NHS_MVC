using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHS_MVC.Models
{
    public class AcceptedAnswer
    {
        public string Type { get; set; }
        public List<MainEntity> mainEntity { get; set; }
    }
    public class MainEntity
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string SubjectOf { get; set; }
        public string Identifier { get; set; }
        public string Text { get; set; }
    }
    public class MainEntityOfPage
    {
        public int Identifier { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }
        public string Headline { get; set; }
        public string Text { get; set; }
        public string Provider { get; set; }
        public string License { get; set; }
        public string DatePublished { get; set; }
        public string subjectOf { get; set; }
        public List<string> Keywords { get; set; }
        public List<MainEntityOfPage> mainEntityOfPage { get; set; }
        public List<MainEntity> MainEntity { get; set; }
        public AcceptedAnswer acceptedAnswer { get; set; }
    }
}
