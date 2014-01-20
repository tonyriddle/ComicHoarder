using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Common
{
    public static class ParseHelper
    {
        public static int ParseInt(string p)
        {
            int i = 0;
            int.TryParse(p, out i);
            return i;
        }

        public static float ParseFloat(string p)
        {
            float i = 0;
            float.TryParse(p, out i);
            return i;
        }

        public static DateTime ParseDateTime(string p)
        {
            DateTime d = new DateTime();
            DateTime.TryParse(p, out d);
            return d;
        }

        public static bool ParseBool(string p)
        {
            if (p == "True")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetEnclosedString(this string s, string begin, string end)
        {
            int beginPos = begin == "" ? 0 : s.IndexOf(begin, 0);
            while (beginPos >= 0)
            {
                int start = beginPos + begin.Length;
                int stop = end == "" ? s.Length : s.IndexOf(end, start);
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
