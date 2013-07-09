using System.Collections.Generic;

namespace ComicHoarder.Common
{
    public interface IEComicService
    {
        List<Issue> GetIssues(string pathname, bool searchSubDirectory);
    }
}
