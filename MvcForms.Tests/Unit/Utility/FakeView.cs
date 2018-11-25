using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeView : IView
    {
        public string Path => throw new System.NotImplementedException();

        public Task RenderAsync(ViewContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
