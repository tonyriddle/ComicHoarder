using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;

namespace ComicHoarder
{
    public class ComicVineURLBuilder : IURLBuilder
    {
        private string key { get; set; }

        public ComicVineURLBuilder(string inkey)
        {
            key = inkey;
        }

        public string PublisherById(int id)
        {
            return "http://www.comicvine.com/api/publisher/4010-" + id + "/?api_key=" + key + "&field_list=volumes,deck,id,name&format=xml";
        }

        public string VolumesFromPublisher(int id)
        {
            return "http://www.comicvine.com/api/publisher/4010-" + id + "/?api_key=" + key + "&field_list=volumes,deck,id,name&format=xml";
        }

        public string VolumeById(int id)
        {
            return "http://www.comicvine.com/api/volume/4050-" + id + "/?api_key=" + key + "&format=xml&field_list=id,publisher,name,deck,date_added,date_last_updated,concept_credits,count_of_issues,start_year,description";
        }

        public string IssuesFromVolume(int id)
        {
            return "http://www.comicvine.com/api/volume/4050-" + id + "/?api_key=" + key + "&format=xml&field_list=id,volume,publisher,name,deck,date_added,date_last_updated,concept_credits,count_of_issues,start_year,issues";
        }

        public string IssueById(int id)
        {
            return "http://www.comicvine.com/api/issue/4000-" + id.ToString() + "/?api_key=" + key + "&format=xml&field_list=id,cover_date,volume,name,issue_number";
        }

        public string SearchVolumes(string name)
        {
            //TODO implement Search Volumes comic vine service
            throw new NotImplementedException();
        }

        public string SearchIssues(string name)
        {
            //TODO implement Search Issue comic vine service
            throw new NotImplementedException();
        }

        public string SearchPublishers(string name)
        {
            return "http://www.comicvine.com/api/search/?api_key=" + key + "&resources=publisher&query=" + name + "&format=xml";
        }

    }
}
