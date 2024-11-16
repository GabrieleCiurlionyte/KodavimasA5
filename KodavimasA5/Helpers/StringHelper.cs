using System.Text.RegularExpressions;

namespace KodavimasA5.Helpers
{
    public static class StringHelper
    {
        public static List<string> SplitStringIntoChunks(string s, int chunkSize)
        {
            List<string> chunks = new List<string>();

            int startIndex = 0;

            while (startIndex < s.Length)
            {
                int endIndex = Math.Min(startIndex + chunkSize, s.Length);

                chunks.Add(s.Substring(startIndex, endIndex - startIndex));

                startIndex = endIndex;
            }

            return chunks;
        }

        public static string EliminateAllWhiteSpaces(string s) 
        {
            return Regex.Replace(s, @"\s", "");
        }
    }
}
