using System;
namespace ComicHoarder.Common
{
    public interface IWebConnection
    {
        string Query(string Url);
    }
}
