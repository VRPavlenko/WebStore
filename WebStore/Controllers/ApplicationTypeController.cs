using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ApplicationTypeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ApplicationTypeController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objList = _db.applicationType;
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
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _db.applicationType.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var obj = _db.applicationType.Find(id);
            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _db.applicationType.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var obj = _db.applicationType.Find(id);
            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(ApplicationType obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            _db.applicationType.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
