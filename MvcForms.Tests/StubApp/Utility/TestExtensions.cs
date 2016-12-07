namespace MvcForms.Tests.StubApp.Utility
{
    public static class TestExtensions
    {
        /// <summary> Removes leading '~' from virtual path </summary>
        public static string PathOnly(this string virtualPath)
        {
            return virtualPath.TrimStart('~');
        }
    }
}
