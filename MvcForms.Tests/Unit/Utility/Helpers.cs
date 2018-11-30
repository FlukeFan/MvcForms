namespace MvcForms.Tests.Unit.Utility
{
    public static class Helpers
    {
        public static FakeHtmlHelper<T> Helper<T>(this T model)
        {
            return FakeHtmlHelper.New(model);
        }
    }
}
