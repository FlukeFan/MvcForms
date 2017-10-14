using System;
using System.Collections.Generic;
using HtmlTags;

namespace MvcForms
{
    public abstract class Styler : IStyler
    {
        public delegate HtmlTag ApplyStyle(object control, HtmlTag tag);

        private static readonly ApplyStyle _applyNoStyle = (c, t) => t;

        private static IStyler _styler = new EmptyStyle();

        public static void Set(IStyler styler)
        {
            _styler = styler;
        }

        public static HtmlTag Style<TControl>(TControl control, HtmlTag tag)
        {
            var styler = _styler.StylerFor(control.GetType());
            return styler(control, tag);
        }

        private IList<Func<Type, ApplyStyle>> _stylers
            = new List<Func<Type, ApplyStyle>>();

        private IDictionary<Type, ApplyStyle> _cachedStylers
            = new Dictionary<Type, ApplyStyle>();

        protected void Register(Func<Type, ApplyStyle> stylerFactory)
        {
            _stylers.Add(stylerFactory);
        }

        protected void Register<TControl>(ApplyStyle styler)
        {
            Register(t => t == typeof(TControl) ? styler : null);
        }

        public virtual ApplyStyle StylerFor(Type type)
        {
            if (!_cachedStylers.ContainsKey(type))
                lock(_cachedStylers)
                {
                    var styler = _applyNoStyle;

                    if (!_cachedStylers.ContainsKey(type))
                    {
                        // most recently added stylers have higher precedence
                        for (var i = _stylers.Count - 1; i >= 0; i--)
                        {
                            var customStyler = _stylers[i](type);
                            styler = customStyler ?? styler;
                        }
                    }

                    _cachedStylers.Add(type, styler);
                }

            return _cachedStylers[type];
        }
    }
}
