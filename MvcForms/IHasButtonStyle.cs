using System.Collections.Generic;

namespace MvcForms
{
    public interface IHasButtonStyle
    {
        IDictionary<string, object> ControlBag          { get; }
        IDictionary<string, object> NullableControlBag  { get; }
    }
}
