using Castle.DynamicProxy;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Utilities.Interceptors
{
    class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttribute<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttribute<MethodInterceptionBaseAttribute>(true);


            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
