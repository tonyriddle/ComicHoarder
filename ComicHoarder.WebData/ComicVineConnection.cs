using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;
using System.IO;
using System.Net;

namespace ComicHoarder.WebData
{
    public class ComicVineConnection : IConnection
    {
        public string Query(string Url)
        {
            using (StreamReader sr = new StreamReader(WebRequest.Create(Url).GetResponse().GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
