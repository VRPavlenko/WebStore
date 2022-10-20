using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;
using WebStore.Models.ViewModels;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db) => _db = db;
        
        public IActionResult Index()
        {
            IEnumerable<Product> objList = _db.Product;
            //get Category
            foreach(var obj in objList)
            {
                obj.Category = _db.Category.FirstOrDefault(u => u.CategoryId == obj.CategoryId);
            }
            return View(objList);
        }

        /// <summary>
        /// update or create (get - upsert)
        /// </summary>
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _db.Category.Select(i => new SelectListItem
                {
                    Text = i.CategoryName,
                    Value = i.CategoryId.ToString()
                })
            };

            //this code use ViewBag -> it`s not ok
            //IEnumerable<SelectListItem> CategoryDropeDown = _db.Category
            //    .Select(i => new SelectListItem
            //    {
            //        Text = i.CategoryName,
            //        Value = i.CategoryId.ToString()
            //    });

            ////get data to view
            //ViewBag.CategoryDropeDown = CategoryDropeDown;

            //Product product = new Product();

            if(id == null)
            {
                //for create
                return View(productVM);
            }
            else
            {
                //for update
                productVM.Product = _db.Product.Find(id);
                if(productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        /// <summary>
        /// post - upsert
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {
            if(ModelState.IsValid) 
            {
                _db.Category.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        /// <summary>
        /// get - delete
        /// </summary>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var obj = _db.Category.Find(id);
            if (obj == null)
                return NotFound();

            return View(obj);
        }

        /// <summary>
        /// post - delete
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Category obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            _db.Category.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
