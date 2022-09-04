using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers {
    public class CategoryController : Controller {

        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db) {
            _db = db;
        }

        public IActionResult Index() {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj) {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The DisplayOrder cannot be the same as the Name.");
            }
            if (ModelState.IsValid) {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(int? id) {
            if (id == null || id == 0) {
                return NotFound();
            }
            var categoryFromFb = _db.Categories.Find(id);
            if (categoryFromFb == null) {
                return NotFound();
            }
            return View(categoryFromFb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj) {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The DisplayOrder cannot be the same as the Name.");
            }
            if (ModelState.IsValid) {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id) {
            if (id == null || id == 0) {
                return NotFound();
            }

            var categoryFromFb = _db.Categories.Find(id);
            if (categoryFromFb == null) {
                return NotFound();
            }

            return View(categoryFromFb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id) {
            var categoryFromFb = _db.Categories.Find(id);
            if (categoryFromFb == null) {
                return NotFound();
            }
            _db.Categories.Remove(categoryFromFb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}