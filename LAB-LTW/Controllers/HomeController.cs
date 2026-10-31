using LAB_LTW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LAB_LTW.Controllers
{
    public class HomeController : Controller
    {
        private readonly LTWQLHangHoaContext db;

        public HomeController(LTWQLHangHoaContext context)
        {
            db = context;
        }

        // Câu 3: Load dữ liệu từ bảng HangHoa có giá trị thuộc tính gia >= 100
        public IActionResult Index()
        {
            var hangHoas = db.HangHoas
                             .Include(h => h.LoaiHang)
                             .Where(h => h.Gia >= 100)
                             .ToList();

            return View(hangHoas);
        }

        // Câu 4: Lọc danh sách HangHoa theo loại và trả về qua AJAX
        public IActionResult GetHangHoaByLoai(int? maloai)
        {
            var query = db.HangHoas.Include(h => h.LoaiHang).Where(h => h.Gia >= 100);

            if (maloai != null && maloai > 0)
            {
                query = query.Where(h => h.MaLoai == maloai);
            }

            var result = query.ToList();
            return PartialView("BuiTuanAnh_MainContent", result);
        }

        // Câu 5.1: Xây dựng chức năng Thêm Hàng Hóa mới (GET)
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(db.LoaiHangs.ToList(), "MaLoai", "TenLoai");
            return View();
        }

        // Câu 5.1 & 5.2: Xây dựng chức năng Thêm Hàng Hóa mới và validate (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HangHoa hangHoa)
        {
            if (ModelState.IsValid)
            {
                db.HangHoas.Add(hangHoa);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MaLoai = new SelectList(db.LoaiHangs.ToList(), "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
