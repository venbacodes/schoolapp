using System;

namespace SchoolApp
{
    public class WaWUrlHelper
    {
        private static readonly char[] QueryStringAndFragmentTokens = new[] { '?', '#' };

        public static bool IsAbsoluteURL(string path)
        {
            var resolvedPath = path;

            var queryStringOrFragmentStartIndex = path.IndexOfAny(QueryStringAndFragmentTokens);

            if (queryStringOrFragmentStartIndex != -1)
            {
                resolvedPath = path.Substring(0, queryStringOrFragmentStartIndex);
            }

            if (Uri.TryCreate(resolvedPath, UriKind.Absolute, out Uri uri) && !uri.IsFile)
            {
                return true;
            }

            return false;
        }
    }
}
