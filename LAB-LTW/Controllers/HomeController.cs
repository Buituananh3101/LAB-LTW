using LAB_LTW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LAB_LTW.Controllers
{
    public class HomeController : Controller
    {
        private LTWQLHangHoaContext db;

        public HomeController(LTWQLHangHoaContext context)
        {
            db = context;
        }

        // [3.1.1]: Load HangHoa có Giá >= 100
        public IActionResult Index()
        {
            var data = db.HangHoas.Where(h => h.Gia >= 100).ToList();
            return View(data);
        }

        // [4.1.1]: Lọc HangHoa theo MaLoai bằng AJAX
        public IActionResult GetHangHoaByLoai(int maloai)
        {
            var data = db.HangHoas.Where(h => h.Gia >= 100);
            if (maloai > 0) data = data.Where(h => h.MaLoai == maloai);
            return PartialView("BuiTuanAnh_MainContent", data.ToList());
        }

        // [5.1.1]: GET - Form thêm hàng hóa mới
        public IActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
            return View();
        }

        // [5.1.3]: POST - Lưu hàng hóa mới
        [HttpPost]
        public IActionResult Create(HangHoa h)
        {
            if (ModelState.IsValid)
            {
                db.HangHoas.Add(h);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoai = new SelectList(db.LoaiHangs, "MaLoai", "TenLoai");
            return View(h);
        }
    }
}
