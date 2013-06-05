using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Common
{
    public interface IEComicConverter
    {
        Issue ConvertToIssue(string xml);
    }
}
