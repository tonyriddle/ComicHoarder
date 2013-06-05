using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Common
{
    public class Volume
    {
        public int id { get; set; }
        public int publisherId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime dateAdded { get; set; }
        public DateTime dateLastUpdated { get; set; }
        public bool collectable { get; set; }
        public int countOfIssues { get; set; }
        public int startYear { get; set; }
        public bool enabled { get; set; }
        public bool complete { get; set; }
    }
}
