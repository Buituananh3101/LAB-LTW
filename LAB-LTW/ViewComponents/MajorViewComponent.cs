using LAB_LTW.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB_LTW.ViewComponents
{
    public class MajorViewComponent: ViewComponent
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
   LUỒNG CHỨC NĂNG LỌC HỌC VIÊN THEO NGÀNH (TÓM TẮT)
============================================================================ */

/* 
   1. TRẠNG THÁI MẶC ĐỊNH (CHƯA LỌC):
   - Trình duyệt gọi URL: /Learner/Index (tham số mid = null).
   - Controller: Lấy TẤT CẢ học viên từ CSDL truyền sang View Index.
   - View Index: 
     + Gọi MajorViewComponent để vẽ thanh Menu Ngành học (mỗi nút bấm chứa link ?mid=ID).
     + Vẽ bảng hiển thị TẤT CẢ học viên.

   2. TRẠNG THÁI KHI BẤM LỌC:
   - Trình duyệt gọi URL: /Learner/Index?mid=1 (ví dụ chọn ngành có ID = 1).
   - Controller: Nhận mid = 1, dùng lệnh .Where() để LỌC ra các học viên thuộc ngành 1.
   - View Index:
     + Vẫn gọi MajorViewComponent để vẽ lại thanh Menu.
     + Vẽ bảng hiển thị danh sách học viên ĐÃ ĐƯỢC LỌC.
*/