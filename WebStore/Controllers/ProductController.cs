using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;
using WebStore.Models.ViewModels;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        
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
        public IActionResult Upsert(ProductVM productVM)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(productVM.Product.Id == 0)
                {
                    //Creating new Product

                    //upload -- path to image
                    string upload = webRootPath + WC.ImagePath;
                    //randome name to img
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    //copy new file to path into "upload"
                    using(var fileStram = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStram);
                    }

                    productVM.Product.ImageUrl = fileName + extension;

                    _db.Product.Add(productVM.Product);
                }
                else
                {
                    //Updating old Product

                    //problem: if dont use AsNoTracking() -> get error:
                    //The instance of entity type 'Product' cannot be tracked because another instance
                    //with the same key value for {'Id'} is already being tracked
                    var objFromDb = _db.Product.AsNoTracking().FirstOrDefault(u => u.Id == productVM.Product.Id);

                    if(files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.ImageUrl);

                        //delete old foto
                        if(System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStram = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStram);
                        }

                        productVM.Product.ImageUrl = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.ImageUrl = objFromDb.ImageUrl;
                    }

                    _db.Product.Update(productVM.Product);

                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            productVM.CategorySelectList = _db.Category.Select(i => new SelectListItem
            {
                Text = i.CategoryName,
                Value = i.CategoryId.ToString()
            });
            return View(productVM);
        }

        /// <summary>
        /// get - delete
        /// </summary>
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Product product = _db.Product.Include(u => u.Category).FirstOrDefault(u => u.Id == id);
            //product.Category = _db.Category.Find(product.CategoryId);
            if (product == null)
                return NotFound();

            return View(product);
        }

        /// <summary>
        /// post - delete
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Product.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + WC.ImagePath;

            var oldFile = Path.Combine(upload, obj.ImageUrl);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Product.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
