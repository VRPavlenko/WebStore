using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

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
            Product product = new Product();
            if(id == null)
            {
                //for create
                return View(product);
            }
            else
            {
                //for update
                product = _db.Product.Find(id);
                if(product == null)
                {
                    return NotFound();
                }
                return View(product);
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
