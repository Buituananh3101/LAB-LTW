using LAB_LTW.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LAB_LTW.Controllers
{
    // Sửa từ mặc đinh [Route("Student")] thành
    [Route("Admin/Student")]
    public class StudentController : Controller
    {
        //------------------------------: Khai báo biến :------------------------------

        private List<Student> Liststudents;

        //------------------------------: Constructor   :------------------------------

        public StudentController() // Constructor khởi tạo, được gọi k
        {
            Liststudents = new List<Student>()
            {
                new Student() { Id = 1, Name = "Nguyen Van A", Email = "nguyenvana@example.com", Password = "password1", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "123 Street, City", DateOfBorth = new DateTime(2000, 1, 1) },
                new Student() { Id = 2, Name = "Tran Thi B", Email = "tranthib@example.com", Password = "password2", Branch = Branch.BE, Gender = Gender.Female, IsRegular = false, Address = "456 Avenue, City", DateOfBorth = new DateTime(2001, 2, 2) },
                new Student() { Id = 3, Name = "Le Van C", Email = "", Password = "password3", Branch = Branch.CE, Gender = Gender.Male, IsRegular = true, Address = "789 Boulevard, City", DateOfBorth = new DateTime(2002, 3, 3) },
                new Student() { Id = 4, Name = "Pham Thi D", Email = "", Password = "password4", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "101 Road, City", DateOfBorth = new DateTime(2003, 4, 4) }
            };
        }

        //------------------------------: Action methods :------------------------------: Hiển thị và thêm sinh viên --> luồng như sau: 
                                                                                          // Khi vào gọi controller/action = student/index
        // GET: /Student/Index --> gọi view Index                                         // --> Hiển thị bảng có link giả nút "Thêm sinh viên", ấn nút
        public IActionResult Index()                                                      // --> Gọi action Create GET của controller hiện tại (được cấu hình như dưới) (tức là hàm ngay dưới hàm này)
        {                                                                                 // < a asp - action = "Create" class="btn btn-primary">Create Student</a>
            return View(Liststudents);                                                    // --> action create GET sẽ chuẩn bị gender, branch và gọi View --> hiển thị form
        }                                                                                 // Trong view, nhập form --> ấn nút gửi
                                                                                          // --> Gọi action create POST --> add và list --> gọi view index (gọi thẳng luôn, ko quay lại hàm bên trái nữa)
        // GET: /Student/Create --> gọi view Create
        // [HttpGet] sửa thành
        [HttpGet("List", Name = "StudentList")] // URL lúc này là Admin/Student/List, Name là tên route để dùng với asp-route.
        public IActionResult Create()
        {
            // Chuẩn bị danh sách giới tính
                                                                // Enum.GetValues(...) Lấy tất cả giá trị trong enum: Male, Female
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)) // typeof(Gender)      Lấy thông tin về kiểu Gender
                                     .Cast<Gender>()            // .Cast<Gender>()     Chuyển từng phần tử sang kiểu Gender
                                     .ToList();                 // .ToList()           Tạo danh sách List<Gender>
                                                                // ViewBag.AllGenders = ... ----> Gửi danh sách này sang View

                                                                // kết quả tương đương:
                                                                // ViewBag.AllGenders = new List<Gender>
                                                                // {
                                                                //     Gender.Male,
                                                                //     Gender.Female
                                                                // };

            // Chuẩn bị danh sách ngành học

            ViewBag.AllBranches = new List<SelectListItem>()    // SelectListItem biểu diễn một lựa chọn trong ô chọn thả xuống:
            {
                new SelectListItem { Value = "1", Text = "IT" },// Text: chữ người dùng nhìn thấy.
                new SelectListItem { Value = "2", Text = "CE" },// Value: giá trị gửi về server khi chọn.
                new SelectListItem { Value = "3", Text = "BE" },
                new SelectListItem { Value = "4", Text = "EE" } // Tương ứng HTML:
            };                                                  // <option value="1">IT</option>

            return View();
        }

        // POST: /Student/Create --> nhận dữ liệu từ form Create + thêm sinh viên mới vào danh sách
                                     // Khi user điền form rồi bấm nút gửi, form được cấu hình như dưới để gọi phương thức này 
                                     // <form asp-controller="Student" asp-action="Create" method="post"> (mở view/Student/Create để hiểu cơ chế model binding)
        // [HttpPost] sửa thành
        [HttpGet("Add", Name = "StudentAdd")] // URL lúc này là Admin/Student/Add, Name = "StudentAdd" để đặt tên cho route này, dùng trong view/Student/Index.cshtml
        public IActionResult create(Student s)
        {
            s.Id = Liststudents.Last<Student>().Id + 1; // Tạo Id mới bằng cách lấy Id của sinh viên cuối cùng trong danh sách và cộng thêm 1
            Liststudents.Add(s);                        // Thêm
            return View("Index", Liststudents);         // Mở View Index.cshtml, truyền danh sách vừa cập nhật làm Model.
                                                        // Dòng này không gọi lại action Index(); nó trực tiếp hiển thị View có tên "Index".
        }





    }
}
