using LAB_LTW.Models;
using Microsoft.AspNetCore.Mvc; // VL phải tự thêm dòng này
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace LAB_LTW.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        //------------------------------: Khai báo biến :------------------------------
        private List<MenuItem> MenuItems;                                               // Chứa các mục trong menu



        //------------------------------: Constructor   :------------------------------
        public RenderViewComponent()                                                   // Khai báo thành phần trong menu 
        {
            MenuItems = new List<MenuItem>                                              // Lưu ý: không có khối {} thì phải giữ (), nếu có {} thì () là optional
            {
                new MenuItem() { Id = 1, Name = "Trang chủ", Link = "/" },
                new MenuItem() { Id = 3, Name = "Student", Link = "/Admin/Student/List" },
                new MenuItem() { Id = 2, Name = "Add Student", Link = "/Admin/Student/Add" }
            };
        }

        //------------------------------: Action methods :------------------------------: 

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu", MenuItems);
        }
    }
}

// HIỂN THỊ MENU BÊN TRÁI --> luồng như sau:
//
// Vào /Admin/Student/List
// --> Gọi action Index của StudentController
// --> return View(Liststudents) --> mở Views/Student/Index.cshtml
//
// Trong Index.cshtml có:
// Layout = "~/Views/Shared/MyLayoutHelper.cshtml";
// --> Dùng MyLayoutHelper làm khung trang
//
// Khi layout chạy tới:
// @await Component.InvokeAsync("Render")
// --> Tìm class RenderViewComponent
// --> Tạo đối tượng --> constructor chạy, tạo danh sách MenuItems
// --> Gọi InvokeAsync()
//
// Trong hàm đó có:
// return View("RenderLeftMenu", MenuItems);
// --> Mở Views/Shared/Components/Render/RenderLeftMenu.cshtml
// --> Truyền danh sách MenuItems sang View, bên View nhận bằng Model
//
// Trong RenderLeftMenu.cshtml:
// @model IEnumerable<MenuItem>
// --> Khai báo View nhận một danh sách MenuItem
//
// @foreach (var item in Model)
// --> Duyệt từng mục trong danh sách
//
// <a class="nav-link" href="@item.Link">@item.Name</a>
// --> Mỗi mục tạo một liên kết:
//     item.Name = chữ hiển thị, ví dụ "Sinh viên"
//     item.Link = địa chỉ mở, ví dụ "/Admin/Student/List"
//
// --> HTML menu được chèn vào chỗ gọi Component trong layout
// --> Trình duyệt nhận trang hoàn chỉnh và hiển thị menu
//
// Khi người dùng bấm một mục menu
// --> Trình duyệt gửi GET đến địa chỉ trong item.Link
// --> Route tìm action tương ứng --> action xử lý và trả về trang
