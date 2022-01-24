using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<Product> GetById(int productId)
        {
            //hatalı bilgi burada dönüyor

            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

       

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());

        }
        public IResult Add(Product product)
        {
            //Magis String ?
            // Business codes kurallar buraya yazılıyor
            _productDal.Add(product);
            return new SuccessResult(Message.ProductAdded);
                
         }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Message.ProductDeleted);

        }



        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Message.ProductUpdated);

        }
    }
}
