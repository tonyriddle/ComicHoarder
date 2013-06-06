using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Common;

namespace ComicHoarder.WebData
{
    public class ComicVineReprintDetector : IComicVineReprintDetector
    {
        public bool DetectReprint(Volume volume)
        {
            if (volume.description.Contains("trade paperback") || volume.description.Contains("tradepaperback") || volume.description.Contains("tpb") || volume.description.Contains("a hardcover book which reprints") || volume.description.Contains("reprinting") || volume.description.Contains("reprints") || volume.description.Contains("collected in the following paperbacks"))
            {
                volume.collectable = false;
                return true;
            }
            else if (volume.description.Contains("collects") || volume.description.Contains("collecting"))
            {
                volume.collectable = false;
                return true;
            }
            return false;
        }
    }
}
