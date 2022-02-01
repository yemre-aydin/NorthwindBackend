using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluendValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //Dependency Injection Altyapısı
        private IProductDal _productDal;
        
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<Product> GetById(int productId)
        {
            //hatalı bilgi burada dönüyor
            Thread.Sleep(5000);
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

       [SecuredOperation("Product.List,Admin")]
        [CacheAspect(10  )]
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());

        }
        //Cross Cutting Concerns - Validation,Cache,Log,Performance,Auth,Transaction

        //AOP-Yazılım geliştirme yaklaşımıdır. Aspect Oriented Programming 
        //cross cutting Concerns de Aop de kullanılır başka yerde kullanma 
        //validation genellikle business in başında kullanılır
        /*
         * Başında Validation yapılır 
         * başında girildiğinde yararlanırız.
         * Transaction Tüm operasyonların baştan sonra doğru çalışmasıdır. hepsinin başarılı olması gerekn durumlar için
         * Cache bazen balında bazen sonnunda yapılır .
         * Log - Loglama başta yada sonda 
         * Performance hem başında hem sonunda yapılır 
         * Auth 
         */


        [ValidationAspect(typeof(ProductValidator),Priority =1)]
        [CacheRemoveAspect("IProductService.Get")]
        [CacheRemoveAspect("ICategoryService.Get")]
        //bizim get olan kısımlar cache de olmalı bunları silecek
        public IResult Add(Product product)
        {

            //Magis String ?
            // Business codes kurallar buraya yazılıyor


            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
                
         }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);

        }



        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);

        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
