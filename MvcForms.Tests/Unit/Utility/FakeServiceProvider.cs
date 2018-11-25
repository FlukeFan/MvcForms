using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcForms.Tests.Unit.Utility
{
    public class FakeServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            if (serviceType.GetGenericTypeDefinition() == typeof(IHtmlHelper<>))
                return GetHtmlHelper(serviceType);

            throw new NotImplementedException($"{GetType()} cannot get service {serviceType}");
        }

        private object GetHtmlHelper(Type serviceType)
        {
            var genericType = serviceType.GetGenericArguments()[0];
            var targetType = typeof(FakeHtmlHelper<>).MakeGenericType(genericType);
            var target = Activator.CreateInstance(targetType, new object[] { null });
            return target;
        }
    }
}
