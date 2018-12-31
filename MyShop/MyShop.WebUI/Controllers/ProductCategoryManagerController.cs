using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMomory;
namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productcategory = context.Collection().ToList();
            return View(productcategory);
        }

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();

        }

        public ActionResult Details(string id)
        {
            ProductCategory productcategory = context.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();

            }
            else
            {

                return View(productcategory);

            }
        }
        public ActionResult Create()
        {
            ProductCategory productcategory = new ProductCategory();
            return View(productcategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productcategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productcategory);
            }

            else
            {
                context.Insert(productcategory);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

        public ActionResult Edit(string id)
        {
            ProductCategory productcategory = context.Find(id);
            if (productcategory == null)
            {
                return HttpNotFound();

            }
            else
            {

                return View(productcategory);

            }

        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productcategory, string Id)
        {
            ProductCategory productcategoryToEdit = context.Find(Id);
            if (productcategoryToEdit == null)
            {
                return HttpNotFound();

            }

            else
            {
                if (ModelState.IsValid)
                {
                    productcategoryToEdit.Category = productcategory.Category;                    
                    context.Commit();
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(productcategory);
                }

            }
        }


        public ActionResult Delete(string Id)
        {
            ProductCategory productcategoryToDelete = context.Find(Id);
            if (productcategoryToDelete == null)
            {
                return HttpNotFound();

            }

            else
            {
                return View(productcategoryToDelete);
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productcategoryToDelete = context.Find(Id);
            if (productcategoryToDelete == null)
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