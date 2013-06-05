using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Common
{
    public class MissingIssue
    {
        public int id { get; set; }
        public int volume_id { get; set; }
        public string volume_name { get; set; }
        public float issue_number { get; set; }
        public int publish_month { get; set; }
        public int publish_year { get; set; }
        public string publisher_name { get; set; }
        public bool collected { get; set; }
        public bool enabled { get; set; }
        public string name { get; set; }
    }
}
