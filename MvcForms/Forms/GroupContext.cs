using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Forms
{
    public class GroupContext
    {
        public static GroupContext New<T>(IHtmlHelper<T> helper, PropertyContext propertyContext, string labelText)
        {
            var modelState = propertyContext.ModelState;

            var hasErrors = modelState != null
                && modelState.Errors != null
                && modelState.Errors.Count > 0;

            return new GroupContext
            {
                Property    = propertyContext,
                LabelText   = labelText,
                HasErrors   = hasErrors,
            };
        }

        protected GroupContext() { }

        public PropertyContext  Property;
        public string           LabelText;
        public bool             HasErrors;
    }
}
