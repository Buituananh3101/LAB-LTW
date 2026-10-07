using Microsoft.AspNetCore.Mvc;
using LAB_LTW.Models;
using Microsoft.EntityFrameworkCore;
namespace LAB_LTW.Controllers
{
    public class LearnerController : Controller
    {
        private LTWSchoolContext db;
        public LearnerController(LTWSchoolContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var learners = db.Learners.Include(m => m.Major).ToList(); // truy vấn lấy toàn bộ danh sách learners từ cơ sở dữ liệu, 
            return View(learners);                                     // đồng thời tự động kết nối (JOIN) để lấy thêm thông tin về chuyên ngành của từng học viên.
        }
    }
}
