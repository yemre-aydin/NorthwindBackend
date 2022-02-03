using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluendValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
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
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
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
        [CacheAspect(10)]
        [LogAspect(typeof(FileLogger))]
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


        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        [CacheRemoveAspect("IProductService.Get")]
        [CacheRemoveAspect("ICategoryService.Get")]
        //bizim get olan kısımlar cache de olmalı bunları silecek
        public IResult Add(Product product)
        {

            //Magis String ?
            // Business codes kurallar buraya yazılıyor

            //ilk kural
            //Eğer kurallar artarsa ne olacak
            //resultları dönüyoruz hep , result fonksiyonları olacak
            //iş çalıştırıcı yazsak onu da Core da yazıcaz
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfCategoryIsEnabled());
            //virgül ekleyip yeni yazılan kurallerı eklenebilir
            if (result != null)
            { 
                return result;

            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

        }

        private IResult CheckIfProductNameExists(string productName)
        {
            if (_productDal.Get(p => p.ProductName == productName) != null)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return null;
        }

        //Kurallar
        //yeni kural eklemek istersek aşağıdaki örnek 
        //örnek olsun diye
        private IResult CheckIfCategoryIsEnabled()
        {
            var result = _categoryService.GetList();
            if (result.Data.Count==10)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);

            }

            return new SuccessResult();
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);

        }



        public IResult Update(Product product)
        {
            if (_productDal.Get(p => p.ProductName == product.ProductName) != null)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
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
