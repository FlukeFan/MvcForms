using System;
using System.Collections.Generic;

namespace MvcForms.Styles
{
    public class CachingStyler : Styler
    {
        private IDictionary<Type, ApplyStyle> _cachedStylers
            = new Dictionary<Type, ApplyStyle>();

        public override ApplyStyle StylerFor(Type type)
        {
            if (!_cachedStylers.ContainsKey(type))
                lock (_cachedStylers)
                {
                    if (!_cachedStylers.ContainsKey(type))
                    {
                        var styler = base.StylerFor(type);
                        _cachedStylers.Add(type, styler);
                    }
                }

            return _cachedStylers[type];
        }
    }
}
