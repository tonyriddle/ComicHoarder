using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Common
{
    public class Issue
    {
        public int id { get; set; }
        public int volumeId { get; set; }
        public string name { get; set; }
        public float issueNumber { get; set; }
        public int publishMonth { get; set; }
        public int publishYear { get; set; }
        public bool collected { get; set; }
        public bool enabled { get; set; }
        public string summary { get; set; }
    }
}
