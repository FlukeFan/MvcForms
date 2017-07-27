using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MvcForms.Tests.SystemTests.Utility
{
    public class WebDriver
    {
        private static IWebDriver   _instance;

        public static IWebDriver Instance()
        {
            if (_instance == null)
                New();

            return _instance;
        }

        public static void Close()
        {
            using (_instance)
                _instance = null;
        }

        private static void New()
        {
            Close();

            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            _instance = new ChromeDriver(service);
        }
    }
}
