using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    interface IProductService
    {
        //tüm listeyi getiren bir operasyon yazılacak
        Product GetById(int productId);
        List<Product> GetList();
        List<Product> GetListByCategory(int categoryId);
        void Add(Product product);
        void Delete(Product product);
        void Update(Product product);

    }
}
