using Castle.Core.Interceptor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Interceptors
{
    //Attribute ile ilgili 
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple =true,Inherited =true)]
    public class MethodInterceptionBaseAttribute:Attribute,IInterceptor
    {
        public int Priority { get; set; }

        public void Intercept(IInvocation invocation)
        {
            
        }

        internal void Intercept(object invocation)
        {
            throw new NotImplementedException();
        }
    }
}
