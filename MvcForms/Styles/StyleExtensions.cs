namespace MvcForms.Styles
{
    public static class StyleExtensions
    {
        public static T Style<T>(this T button, ButtonStyle style)
            where T : IHasButtonStyle
        {
            button.ControlBag[ButtonStyle.Key] = style;
            return button;
        }
    }
}
