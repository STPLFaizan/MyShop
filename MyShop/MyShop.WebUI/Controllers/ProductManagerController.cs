using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

using MyShop.DataAccess.InMomory;
namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        ProductCategoryRepository productcategories;
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ProductManagerController()
        {
            context = new ProductRepository();
            productcategories = new ProductCategoryRepository();

        }

        public ActionResult Details(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();

            }
            else
            {

                return View(product);

            }
        }
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = new Product();
            viewModel.productcategory = productcategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
             }

            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Edit(string id)
        {
            Product product = context.Find(id);
            if (product == null)
            {
                return HttpNotFound();

            }
            else {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.product = product;
                viewModel.productcategory = productcategories.Collection();
                return View(viewModel);

            }   

        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();

            }
           
            else
            {
                if (ModelState.IsValid)
                {
                    productToEdit.Categorty = product.Categorty;
                    productToEdit.Description = product.Description;
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price ;
                    productToEdit.Categorty = product.Categorty;
                    productToEdit.Image = product.Image;
                    context.Commit();
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(product);
                }

            }
        }


        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();

            }

                else
                {
                    return View(productToDelete);
                }

            }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();

            }

            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}