namespace MvcForms.Styles.Bootstrap
{
    public class BootstrapButton : ButtonStyle
    {
        protected BootstrapButton() { }

        public static readonly BootstrapButton Default  = new BootstrapButton();
        public static readonly BootstrapButton Primary  = new BootstrapButton();
        public static readonly BootstrapButton Success  = new BootstrapButton();
        public static readonly BootstrapButton Info     = new BootstrapButton();
        public static readonly BootstrapButton Warning  = new BootstrapButton();
        public static readonly BootstrapButton Danger   = new BootstrapButton();
    }
}
