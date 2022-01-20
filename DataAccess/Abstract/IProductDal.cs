using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;



namespace DataAccess.Abstract
    //veriye erişim sağlayacağımız dataaccess katmanı

{
    public interface IProductDal : IEntityRepository<Product>
    {
    }
}
