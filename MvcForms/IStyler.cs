namespace MvcForms
{
    public interface IStyler
    {
        Styler.ApplyStyle StylerFor(IControl control);
    }
}
