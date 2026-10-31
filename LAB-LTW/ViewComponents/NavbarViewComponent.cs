using LAB_LTW.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB_LTW.ViewComponents
{
    // 2.2.1: ViewComponent phụ trách lấy dữ liệu LoaiHang từ Model theo chuẩn MVC (Views/Shared/Components/Navbar/Default.cshtml - 4)
    public class NavbarViewComponent : ViewComponent
    {
        private LTWQLHangHoaContext db;

        public NavbarViewComponent(LTWQLHangHoaContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke()
        {
            var loaiHangs = db.LoaiHangs.ToList();
            return View(loaiHangs);
        }
    }
}
