using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
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
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
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

            //Category? categoryFromDb = _db.Categories.Find(id); //sadece primary keyle çalışır

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

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
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
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

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id); //sadece primary keyle çalışır
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id); //kayıt varsa kaydı döndürür; yoksa null object döner.
            //                                                                            //her türlü alanı arayabilirsin, contains filan burada.

            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault(); //ikinciye benziyo
            if (categoryFromDb == null) { return NotFound(); }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _unitOfWork.Category.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index", "Category");

        }
    }
}
