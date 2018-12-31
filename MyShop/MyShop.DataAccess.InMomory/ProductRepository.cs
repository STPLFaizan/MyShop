using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;
namespace MyShop.DataAccess.InMomory
{
   public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()

        {

            products = cache["products"] as List<Product>;
            if (products == null)

            {
                products = new List<Product>();
            }

        }
        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product p)
        {
            Product productsforupdate = products.Find(p1 => p1.Id == p.Id);
            if (productsforupdate != null)
            {
                productsforupdate = p;
            }
            else
            {

                throw new Exception("Product Not Found");
            }
        }

        public Product Find(string id)
        {
            Product productstofind = products.Find(p1 => p1.Id == id);
            if (productstofind != null)
            {
                return productstofind;
            }
            else
            {

                throw new Exception("Product Not Found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product productsforDelete = products.Find(p1 => p1.Id == Id);
            if (productsforDelete != null)
            {
                products.Remove(productsforDelete);
            }
            else
            {

                throw new Exception("Product Not Found");
            }
        }

    }
}
