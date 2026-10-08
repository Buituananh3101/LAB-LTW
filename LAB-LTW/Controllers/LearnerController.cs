using LAB_LTW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace LAB_LTW.Controllers
{
    public class LearnerController : Controller
    {
        //-------------------------------------------------------------------------------------------: Khởi tạo
        private LTWSchoolContext db;
        public LearnerController(LTWSchoolContext context)
        {
            db = context;
        }
        //-------------------------------------------------------------------------------------------: List Learners
        public IActionResult Index()
        {
            var learners = db.Learners.Include(m => m.Major).ToList(); // truy vấn lấy toàn bộ danh sách learners từ cơ sở dữ liệu, 
            return View(learners);                                     // đồng thời tự động kết nối (JOIN) để lấy thêm thông tin về chuyên ngành của từng học viên.
        }

        //-------------------------------------------------------------------------------------------: Create Learner

        // GET: Learner/Create
        public IActionResult Create()
        {
            //dùng 1 trong 2 cách để tạo SelectList gửi về View qua ViewBag để
            //hiển thị danh sách chuyên ngành (Majors)
            var majors = new List<SelectListItem>(); //cách 1
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,
                    Value = item.MajorID.ToString()
                });
            }
            ViewBag.MajorID = majors;
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName"); //cách 2
            return View();
        }

        // POST: Learner/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // là một thuộc tính (attribute) bảo mật trong ASP.NET Core dùng để ngăn chặn các cuộc tấn công Giả mạo yêu cầu liên trang
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //lại dùng 1 trong 2 cách tạo SelectList gửi về View để hiển thị danh sách Majors
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }
    }

}
