using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoReCaptcha2
{
    internal static class TextParse
    {
        // text - text, label - label by which we are looking for the right place
        // strtStr - the place we are looking for starting after the label, endStr - the string that comes immediately after the substring we need
        // returns the substring that is between strtStr and endStr, if not found - null
        public static string SubString(string text, string label, string strtStr, string endStr)
        {
            int strtIndex = text.IndexOf(label);
            if (strtIndex < 0) return null;
            strtIndex += label.Length;
            if ((strtIndex = text.IndexOf(strtStr, strtIndex)) < 0) return null;
            strtIndex += strtStr.Length;

            int endIndex = text.IndexOf(endStr, strtIndex);
            if (endIndex < 0) return null;

            return text.Substring(strtIndex, endIndex - strtIndex);
        }

        // returns the substring that is between strtStr and endStr, if not found - null
        public static string SubString(string text, string strtStr, string endStr)
        {
            int strtIndex = text.IndexOf(strtStr);
            if (strtIndex < 0) return null;
            strtIndex += strtStr.Length;

            int endIndex = text.IndexOf(endStr, strtIndex);
            if (endIndex < 0) return null;

            return text.Substring(strtIndex, endIndex - strtIndex);
        }


        // returns the substring that is between strtStr and the end of the string
        public static string SubString(string text, string strtStr)
        {
            int strtIndex = text.IndexOf(strtStr);
            if (strtIndex < 0) return null;
            strtIndex += strtStr.Length;

            int endIndex = text.Length;
            if (endIndex < 0) return null;

            return text.Substring(strtIndex, endIndex - strtIndex);
        }
    }
}
