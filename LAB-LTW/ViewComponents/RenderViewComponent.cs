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

// Luồng viewcomponent:

//          Hiển thị LeftMenu động: thêm các mục vào List<MenuItem> thoải mái --> luồng như sau:
//          Khi gọi controller/action Student/Index --> mở view Student Index
//          Trong view Student Index khai báo Layout = "~/Views/Shared/MyLayoutHelper.cshtml"; --> dùng Layout này
//          Khi Layout chạy tới @await Component.InvokeAsync("Render") --> tìm ViewComponent RenderViewComponent và thực hiện lần lượt
//                  1. Chạy connstructor tạo List<MenuItem>
//                  2. Gọi InvokeAsync() --> return View("RenderLeftMenu", MenuItems) --> mở view RenderLeftMenu
//         Trong view RenderLeftMenu khai báo @model IEnumerable<MenuItem> --> foreach các menu trong list menu ra như dưới

//@foreach(var i in Model)
//{
//    < a class= "nav-link" href = "@i.Link" >
//        < div class= "sb-nav-link-icon" >< i class= "fas fa-chart-area" ></ i ></ div >
//        @i.Name
//    </ a >
//}

//         Khi ấn vào thì sẽ gửi GET đến địa chỉ trong i.Link --> route tìm action tương ứng --> action xử lý và trả về trang
