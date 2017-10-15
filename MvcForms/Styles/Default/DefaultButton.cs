namespace MvcForms.Styles.Default
{
    public class DefaultButton : ButtonStyle
    {
        protected DefaultButton() { }

        public static readonly DefaultButton Default    = new DefaultButton();
        public static readonly DefaultButton Primary    = new DefaultButton();
        public static readonly DefaultButton Accent     = new DefaultButton();
    }
}
