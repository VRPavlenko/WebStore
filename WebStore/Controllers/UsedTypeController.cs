using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class UsedTypeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public UsedTypeController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            IEnumerable<UsedType> objList = _db.UsedType;
            return View(objList);
        }

        /// <summary>
        /// get - create
        /// </summary>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// set - create
        /// </summary>
        [HttpPost]
        //input security check
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsedType obj)
        {
            if (ModelState.IsValid)
            {
                _db.UsedType.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var obj = _db.UsedType.Find(id);
            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UsedType obj)
        {
            if (ModelState.IsValid)
            {
                _db.UsedType.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var obj = _db.UsedType.Find(id);
            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(UsedType obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            _db.UsedType.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
