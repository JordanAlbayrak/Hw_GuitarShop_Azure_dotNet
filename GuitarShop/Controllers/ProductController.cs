using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GuitarShop.Models;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        private ShopContext context;

        public ProductController(ShopContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List", "Product");
        }

        [Route("[controller]s/{id?}")]
        public IActionResult List(string id = "All")
        {
            var categories = context.Categories
                .OrderBy(c => c.CategoryID).ToList();

            List<Product> products;
            if (id == "All")
            {
                products = context.Products
                    .OrderBy(p => p.ProductID).ToList();
            }
            else if (id.Equals("Strings")){  //Boolean statement that checkes whether the id is equal to "Strings"

                //LINQ statement that gets the products under the names of either Guitars or
                //Basses and orders them by id to then be sent back to the view
                products = context.Products
                .Where(p => p.Category.Name == "Guitars" | p.Category.Name == "Basses")
                .OrderBy(p => p.ProductID).ToList();
            }
            else
            {
                products = context.Products
                    .Where(p => p.Category.Name == id)
                    .OrderBy(p => p.ProductID).ToList();

            }

            // use ViewBag to pass data to view
            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryName = id;

            //If statement to set SelectedCategoryName to "Strings" if the id is equal to that.
            if (id.Equals("Strings"))
            {
                ViewBag.SelectedCategoryName = id;
            }

            // bind products to view
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var categories = context.Categories
                .OrderBy(c => c.CategoryID).ToList();

            Product product = context.Products.Find(id);

            string imageFilename = product.Code + "_m.png";

            // use ViewBag to pass data to view
            ViewBag.Categories = categories;
            ViewBag.ImageFilename = imageFilename;

            // bind product to view
            return View(product);
        }
    }
}