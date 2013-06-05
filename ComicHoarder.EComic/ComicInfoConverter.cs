using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;
using System.Xml.Serialization;
using System.IO;

namespace ComicHoarder.EComic
{
    public class ComicInfoConverter : IEComicConverter
    {

        public Issue ConvertToIssue(string xml)
        {
            ComicInfo info = ConvertToCRInfo(xml);
            Issue issue = new Issue();
            
            string id = GetCVIDFromNotes(info.Notes);
            int issueId = 0;
            int.TryParse(id, out issueId);
            issue.id = issueId;
            issue.volumeId = 0; //TODO can possibly pull from db if already there using issueid - or pull from cv using series and volume
            issue.name = info.Title;
            issue.issueNumber = ParseHelper.ParseInt(info.Number);
            issue.publishMonth = ParseHelper.ParseInt(info.Month);
            issue.publishYear = ParseHelper.ParseInt(info.Year);
            issue.collected = true;
            issue.enabled = true;
            issue.summary = info.Summary;
            return issue;
        }

        public ComicInfo ConvertToCRInfo(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ComicInfo));
            TextReader reader = new StringReader(xml);
            ComicInfo info = (ComicInfo)serializer.Deserialize(reader);
            return info;
        }

        public string GetCVIDFromNotes(string s)
        {
            string begin = "[CVDB";
            string end = "]";
            int beginPos = s.IndexOf(begin, 0);
            while (beginPos >= 0)
            {
                int start = beginPos + begin.Length;
                int stop = s.IndexOf(end, start);
                if (stop < 0)
                {
                    return "";
                }
                return s.Substring(start, stop - start);
            }
            return "";
        }
    }
}
