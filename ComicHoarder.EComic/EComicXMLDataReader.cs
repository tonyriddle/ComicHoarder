using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;
using System.Xml.Linq;

namespace ComicHoarder.EComic
{
    public class EComicXMLDataReader : IEComicDataReader
    {
        public Issue ReadIssueData(string comicInfo)
        {
            var issue = new Issue();
            var XDoc = XDocument.Parse(comicInfo);

            var notes = (string)XDoc.Descendants("Notes").Where(p => p.Parent.Name.LocalName == "ComicInfo").FirstOrDefault();
            if (notes != null)
            {
                string id = GetCVIDFromNotes(notes);
                int issueId = 0;
                int.TryParse(id, out issueId);

                issue.id = issueId;
                issue.volumeId = 0; //TODO can possibly pull from db if already there using issueid - or pull from cv using series and volume
                                    //should be able to query Volume based on issue series name and volume number
                issue.name = (string)XDoc.Descendants("Title").Where(p => p.Parent.Name.LocalName == "ComicInfo").FirstOrDefault() ?? "";
                issue.issueNumber = ParseHelper.ParseInt((string)XDoc.Descendants("Number").Where(p => p.Parent.Name.LocalName == "ComicInfo").FirstOrDefault() ?? "");
                //handle issueNumberSuffix

                issue.publishMonth = ParseHelper.ParseInt((string)XDoc.Descendants("Month").Where(p => p.Parent.Name.LocalName == "ComicInfo").FirstOrDefault() ?? "");
                issue.publishYear = ParseHelper.ParseInt((string)XDoc.Descendants("Year").Where(p => p.Parent.Name.LocalName == "ComicInfo").FirstOrDefault() ?? "");
                issue.summary = (string)XDoc.Descendants("Summary").Where(p => p.Parent.Name.LocalName == "ComicInfo").FirstOrDefault() ?? "";
                issue.collected = true;
                issue.enabled = true;
            }

            return issue;
        }

        public string GetCVIDFromNotes(string Notes)
        {
            string begin = "[CVDB";
            string end = "]";
            int beginPos = Notes.IndexOf(begin, 0);
            while (beginPos >= 0)
            {
                int start = beginPos + begin.Length;
                int stop = Notes.IndexOf(end, start);
                if (stop < 0)
                {
                    return "";
                }
                return Notes.Substring(start, stop - start);
            }
            return "";
        }
    }
}
