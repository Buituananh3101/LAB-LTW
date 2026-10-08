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

        //-------------------------------------------------------------------------------------------: Update Learner

        // Get : Learner/Edit/5
        public IActionResult Edit(int id) // (Lưu ý:  để int? id tránh lỗi id == null)
        {
            // nếu không có ID truyền vào URL hoặc bảng Learners chưa được khởi tạo
            if (id == null || db.Learners == null)
            {
                return NotFound(); // Trả về trang lỗi 404 (Không tìm thấy)
            }

            // Tìm kiếm học viên trong CSDL theo ID khóa chính
            var learner = db.Learners.Find(id);

            // Nếu không tìm thấy học viên nào có ID tương ứng
            if (learner == null)
            {
                return NotFound();
            }

            // Tạo danh sách thả xuống (Dropdown) chứa các Chuyên ngành (Majors).
            // Tham số cuối (learner.MajorID) giúp hệ thống tự động chọn (selected) đúng ngành cũ của học viên này trên Form.
            ViewBag.MajorId = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);

            // Trả về View của học viên vừa tìm được để điền sẵn vào Form
            return View(learner);
        }

        // Post: Learner/Edit/5
        // Action này chạy khi người dùng bấm nút "Lưu/Submit" trên Form Edit để gửi dữ liệu mới lên máy chủ.
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner) // Dùng [Bind] để chỉ định chính xác các trường dữ liệu được phép nhận
        {
            // Kiểm tra bảo mật: Xem ID trên URL có khớp với ID ẩn bên trong Form gửi lên không
            if (id != learner.LearnerID)
            {
                return NotFound();
            }

            // Kiểm tra xem dữ liệu nhập vào có hợp lệ không (vd: có bỏ trống trường bắt buộc không)
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner); // Đánh dấu bản ghi learner này là đã bị thay đổi
                    db.SaveChanges();   // Thực thi lệnh UPDATE xuống SQL Server để lưu thay đổi
                }
                catch (DbUpdateConcurrencyException) // Bắt lỗi tương tranh dữ liệu (ví dụ: 2 người cùng sửa 1 học viên cùng lúc)
                {
                    // Nếu lỗi xảy ra do học viên này vừa bị người khác xóa mất rồi
                    if (!LearnerExists(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        // Nếu do lỗi khác, thì ném lỗi ra hệ thống xử lý tiếp
                        throw;
                    }
                }
                // Nếu lưu thành công, chuyển hướng người dùng về lại trang danh sách (Action Index)
                return RedirectToAction(nameof(Index));
            }

            // Nếu dữ liệu không hợp lệ (ModelState.IsValid = false), tạo lại danh sách Dropdown 
            // và hiển thị lại Form (kèm thông báo lỗi) để người dùng sửa lại
            ViewBag.MajorId = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        // Helper method
        // kiểm tra nhanh một học viên có trong CSDL hay không dựa vào ID
        private bool LearnerExists(int id)
        {
            // true nếu bảng Learners có chứa ít nhất 1 bản ghi khớp với id truyền vào, ngược lại trả về false
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
        }

        //-------------------------------------------------------------------------------------------: Delete Learner

        // 1. GET: Learner/Delete/5
        // Action này chạy khi bạn bấm nút "Delete" trên danh sách. Nó kiểm tra điều kiện
        // và hiển thị trang xác nhận thông tin trước khi thực sự xóa.
        public IActionResult Delete(int id) // (Lưu ý:  để int? id tránh lỗi id == null)
        {
            // nếu không có ID truyền vào URL hoặc bảng Learners chưa được khởi tạo
            if (id == null || db.Learners == null)
            {
                return NotFound(); // // Trả về trang lỗi 404 (Không tìm thấy)
            }

            // Truy vấn tìm học viên theo ID, dùng Include để tự động JOIN và lấy thêm thông tin Ngành học (Major) và các Khóa học đã đăng ký (Enrollments)
            var learner = db.Learners.Include(l => l.Major)
                .Include(e => e.Enrollments)
                .FirstOrDefault(m => m.LearnerID == id);

            // Nếu không tìm thấy học viên nào có ID tương ứng
            if (learner == null)
            {
                return NotFound();
            }

            // KIỂM TRA RÀNG BUỘC: Nếu học viên này đã đăng ký ít nhất 1 khóa học (Enrollments > 0) --> chặn lại không cho xóa để bảo vệ toàn vẹn dữ liệu (tránh lỗi khóa ngoại).
            if (learner.Enrollments.Count() > 0)
            {
                return Content("Thằng này đăng kí học rồi, ko được xóa để bảo vệ toàn vẹn dữ liệu!");
            }

            // Trả về View xác nhận xóa, hiển thị thông tin học viên để người dùng xem xét lần cuối
            return View(learner);
        }

        // 2. POST: Learner/Delete/5
        // Action này nhận dữ liệu khi người dùng bấm nút "Xác nhận xóa" từ form (nút Submit).
        [HttpPost, ActionName("Delete")] // Mapping tên action: Dù tên hàm là DeleteConfirmed, nhưng URL vẫn gọi là "Delete"
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Kiểm tra an toàn xem bảng Learners có tồn tại không
            if (db.Learners == null)
            {
                return Problem("Entity set 'Learners' is null.");
            }

            // Tìm bản ghi học viên cần xóa theo ID
            var learner = db.Learners.Find(id);

            // Nếu tìm thấy, gọi hàm Remove để đánh dấu bản ghi này sẽ bị xóa
            if (learner != null)
            {
                db.Learners.Remove(learner);
            }

            // Lệnh này mới thực sự đẩy câu lệnh SQL DELETE xuống CSDL để xóa cứng dữ liệu
            db.SaveChanges();

            // Xóa xong thì chuyển hướng người dùng quay về trang danh sách (Action Index)
            return RedirectToAction(nameof(Index));
        }

        //-------------------------------------------------------------------------------------------:
    }

}
