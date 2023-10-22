using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name");
            }


            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id); //sadece primary keyle çalışır
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id); //kayıt varsa kaydı döndürür; yoksa null object döner.
            //                                                                            //her türlü alanı arayabilirsin, contains filan burada.

            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); //ikinciye benziyo
            if (categoryFromDb == null) { return NotFound(); }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {



            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id); //sadece primary keyle çalışır
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id); //kayıt varsa kaydı döndürür; yoksa null object döner.
            //                                                                            //her türlü alanı arayabilirsin, contains filan burada.

            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); //ikinciye benziyo
            if (categoryFromDb == null) { return NotFound(); }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");

        }
    }
}
