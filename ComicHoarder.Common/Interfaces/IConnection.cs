using System;
namespace ComicHoarder.Common
{
    public interface IConnection
    {
        string Query(string Url);
    }
}
