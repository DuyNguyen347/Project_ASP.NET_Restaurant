using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var CategoryList = _db.Categories.ToList();
            return View(CategoryList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // Mặc định tên Action sẽ trùng với tên hàm bên dưới, nếu muốn đổi lại tên action khác với tên hàm thì dùng lệnh
        // [HttpPost,ActionName("ten action")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            bool isCheck = _db.Categories.Any(c => c.Name == obj.Name);
            if (isCheck == true) ModelState.AddModelError("duplicateName", "Exist that name in database");
            // bởi vì AddModelError đã thêm vào Error có tên duplicateName nên ModelState.IsValid sẽ trả về False nêu trùng thê trong db
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Create successed";
                return RedirectToAction("Index");
            }
            else return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            // có thể dùng các phương thức khác như single, singleOrDefault, first, firstOrDefault
            var categoryById = _db.Categories.Find(id);
            if (categoryById == null) return NotFound();
            return View(categoryById);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            
            // bởi vì AddModelError đã thêm vào Error có tên duplicateName nên ModelState.IsValid sẽ trả về False nêu trùng thê trong db
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Edit successed";
                return RedirectToAction("Index");
            }
            else return View();
        }

        [HttpPost,ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var categoryById = _db.Categories.Find(id);
            if(categoryById == null) return NotFound();
            // bởi vì AddModelError đã thêm vào Error có tên duplicateName nên ModelState.IsValid sẽ trả về False nêu trùng thê trong db
            if (ModelState.IsValid)
            {
                _db.Categories.Remove(categoryById);
                _db.SaveChanges();
                TempData["success"] = "Delete successed";
                return RedirectToAction("Index");
            }
            else return RedirectToAction("Index");
        }

    }
}
