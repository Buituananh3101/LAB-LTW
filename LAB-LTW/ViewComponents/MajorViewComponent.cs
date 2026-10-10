using LAB_LTW.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB_LTW.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        LTWSchoolContext db;
        List<Major> majors;

        public MajorViewComponent(LTWSchoolContext _context)
        {
            db = _context;
            majors = db.Majors.ToList();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderMajor", majors);
        }
    }
}

/* ============================================================================
   LUỒNG HOẠT ĐỘNG: LỌC DỮ LIỆU BẤT ĐỒNG BỘ (AJAX & ASYNC)
============================================================================ */

/* 
   1. KÍCH HOẠT VÀ GỬI YÊU CẦU NGẦM (FRONT-END AJAX):
   - Kích hoạt: Người dùng bấm vào thẻ <li> (Ngành học) trên thanh Menu.
   - Xử lý sự kiện: jQuery lấy mã ngành (id) của thẻ <li> vừa được bấm.
   - Gọi AJAX: Gửi ngầm một gói tin chứa tham số 'mid' lên Server (địa chỉ LearnerByMajorID).
     + KHÔNG F5: Trình duyệt không tải lại trang, màn hình không bị chớp hay đóng băng. Người dùng vẫn thao tác được bình thường.

   2. XỬ LÝ DỮ LIỆU VÀ GIẢI PHÓNG LUỒNG (BACK-END SERVER):
   - Controller: Action LearnerByMajorID nhận tham số 'mid' và dùng lệnh .Where() để LỌC học viên.
   - Trả kết quả: KHÔNG trả về cả trang web to, mà CHỈ trả về một phần giao diện nhỏ (PartialView: LearnerTable.cshtml) chứa bảng dữ liệu đã lọc.
   - Tính Async: Các hàm có từ khóa 'async Task' sẽ tự động giải phóng luồng xử lý (thread) trong lúc chờ CSDL tìm dữ liệu, giúp Server không bị nghẽn khi có nhiều người truy cập cùng lúc.

   3. NHẬN KẾT QUẢ VÀ CẬP NHẬT GIAO DIỆN (FRONT-END AJAX):
   - Đón dữ liệu: AJAX báo 'success' và nhận về cục mã HTML (response) từ Server ném xuống.
   - Ghi đè giao diện: Dùng jQuery $("div#content").html(response) để bê nguyên cục HTML mới nhét đè vào khung bảng cũ.
   => KẾT QUẢ: Danh sách học viên được cập nhật mượt mà ngay trên trang hiện tại.



    Tự giải thích: 
    Ban đầu gọi contrller/action Learner/index --> gọi view Learner/index.
        Trong view này, có gọi VC MajorViewComponent bằng "@await Component.InvokeAsync("Major")"
        Trong MajorVC lấy majors từ CSLD + trả về view "RenderMajor" 
        Trong view "RenderMajor" có render ra các thẻ <li> (Ngành học) trên thanh Menu, thẻ li có chứa mã ngành (id) của ngành học.
    Tiếp theo khi đã có menu ngành học, trong view index thêm phần @section Scripts, trong này dùng jquey để bắt sự kiện click vào thẻ li.
        lấy id ngành từ thẻ li rồi gọi action LearnerByMajorID, thằng này tìm leanrs rồi trả về PartialView LearnerTable
        AJAX lấy cái PartialView LearnerTable đó vào thay vào khối content

*/