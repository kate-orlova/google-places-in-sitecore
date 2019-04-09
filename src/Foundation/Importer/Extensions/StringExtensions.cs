using System;
using System.Collections.Generic;
using System.Linq;
using Glass.Mapper;

namespace Importer.Extensions
{
    public static class StringExtensions
    {
        public static string[] SafeSplit(this string value, string separator)
        {

            if (string.IsNullOrEmpty(value))
            {
                return Enumerable.Empty<string>().ToArray();
            }

            return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
