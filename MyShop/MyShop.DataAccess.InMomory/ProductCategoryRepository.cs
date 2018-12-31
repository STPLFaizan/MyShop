using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;
using System.Runtime.Caching;
namespace MyShop.DataAccess.InMomory
{
    public  class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productscategory = new List<ProductCategory>();

        public ProductCategoryRepository()

        {

            productscategory = cache["productscategory"] as List<ProductCategory>;
            if (productscategory == null)

            {
                productscategory = new List<ProductCategory>();
            }

        }
        public void Commit()
        {
            cache["productscategory"] = productscategory;
        }

        public void Insert(ProductCategory p)
        {
            productscategory.Add(p);
        }

        public void Update(ProductCategory p)
        {
            ProductCategory productsforupdate = productscategory.Find(p1 => p1.Id == p.Id);
            if (productsforupdate != null)
            {
                productsforupdate = p;
            }
            else
            {

                throw new Exception("Product Category Not Found");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory productscategorytofind = productscategory.Find(p1 => p1.Id == id);
            if (productscategorytofind != null)
            {
                return productscategorytofind;
            }
            else
            {

                throw new Exception("Product Category Not Found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productscategory.AsQueryable();
        }   

        public void Delete(string Id)
        {
            ProductCategory productscategoryforDelete = productscategory.Find(p1 => p1.Id == Id);
            if (productscategoryforDelete != null)
            {
                productscategory.Remove(productscategoryforDelete);
            }
            else
            {

                throw new Exception("Product Category Not Found");
            }
        }

    }
}
