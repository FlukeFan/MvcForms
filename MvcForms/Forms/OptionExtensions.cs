using System.Collections.Generic;

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
    }
}
