using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcForms.Forms
{
    public static class OptionExtensions
    {
        public static IEnumerable<Option> Optional(this IEnumerable<Option> options, string text)
        {
            yield return Option.Value(null, text);

            foreach (var option in options)
                yield return option;
        }

        public static IEnumerable<Option> ToOptions<T>(this IEnumerable<KeyValuePair<T, string>> options)
        {
            return options.Select(kvp => Option.Value(Convert.ToString(kvp.Key), kvp.Value));
        }
    }
}
