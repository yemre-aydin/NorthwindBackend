using Business.Abstract;
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
        public void Add(Product product)
        {
           // Business codes kurallar buraya yazılıyor

        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public Product GetById(int productId)
        {
            
            return _productDal.Get(p => p.ProductId == productId);
        }

        public List<Product> GetList()
        {
            return _productDal.GetList().ToList();
        }

        public List<Product> GetListByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
