using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect:MethodInterception
    {
        private int _duration;
        ICacheManager _cacheManager;

        public CacheAspect(int duration=60)//değer girilmesi için verilen süre
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        //ProductManager.GetByCategory(1,"sdsfsf")
        //kendimizce bir key oluşturduk bu bize farklı parametreler gelirse key değiştirmiş oluyoruz
        //operasyon bazlı bir caching altyapısı oluşturmuş oluyoruz.
        public override void Intercept(IInvocation invocation)
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            if (_cacheManager.IsAdd(key))
            {
                invocation.ReturnValue = _cacheManager.Get(key);//cache de var sa burası çalışıyor
                return;
            }
            invocation.Proceed();//cache de yoksa burası çalışıyor
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
            
        }
    }
}
