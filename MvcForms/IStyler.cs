using System;

namespace MvcForms
{
    public interface IStyler
    {
        Styler.ApplyStyle StylerFor(Type type);
    }
}
