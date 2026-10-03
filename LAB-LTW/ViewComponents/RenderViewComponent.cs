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
