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
        private readonly IWebHostEnvironment _env;

        //------------------------------: Constructor   :------------------------------

        public StudentController(IWebHostEnvironment env) // Constructor khởi tạo, được gọi khi tạo đối tượng controller
        {
            _env = env;
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
        [HttpGet("List", Name = "StudentList")]
        public IActionResult Index()                                                      // --> Gọi action Create GET của controller hiện tại (được cấu hình như dưới) (tức là hàm ngay dưới hàm này)
        {                                                                                 // < a asp - action = "Create" class="btn btn-primary">Create Student</a>
            return View(Liststudents);                                                    // --> action create GET sẽ chuẩn bị gender, branch và gọi View --> hiển thị form
        }                                                                                 // Trong view, nhập form --> ấn nút gửi
                                                                                          // --> Gọi action create POST --> add và list --> gọi view index (gọi thẳng luôn, ko quay lại hàm bên trái nữa)
                                                                                          // GET: /Student/Create --> gọi view Create
        // [HttpGet] sửa thành
        [HttpGet("Add", Name = "StudentAdd")] // URL lúc này là Admin/Student/List, Name là tên route để dùng với asp-route.
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

        //// POST: /Student/Create --> nhận dữ liệu từ form Create + thêm sinh viên mới vào danh sách
        //                             // Khi user điền form rồi bấm nút gửi, form được cấu hình như dưới để gọi phương thức này 
        //                             // <form asp-controller="Student" asp-action="Create" method="post"> (mở view/Student/Create để hiểu cơ chế model binding)
        //// [HttpPost] sửa thành
        //[HttpPost("Add", Name = "StudentAddPost")] // URL lúc này là Admin/Student/Add, Name = "StudentAdd" để đặt tên cho route này, dùng trong view/Student/Index.cshtml
        //public IActionResult create(Student s)
        //{
        //    s.Id = Liststudents.Last<Student>().Id + 1; // Tạo Id mới bằng cách lấy Id của sinh viên cuối cùng trong danh sách và cộng thêm 1
        //    Liststudents.Add(s);                        // Thêm
        //    return View("Index", Liststudents);         // Mở View Index.cshtml, truyền danh sách vừa cập nhật làm Model.
        //                                                // Dòng này không gọi lại action Index(); nó trực tiếp hiển thị View có tên "Index".
        //}

        // Là cái hàm bị comment bên trên, tuy nhiên hàm dưới này được thêm code phần upload ảnh
        [HttpPost("Add", Name = "StudentAddPost")]
        public async Task<IActionResult> Create(Student s, IFormFile? avatar)
        {
            //------------------------------: Xử lý upload ảnh :------------------------------: begin


            if (avatar != null && avatar.Length > 0)                                      // Chỉ xử lý khi người dùng đã chọn file và file không rỗng.
            {                                                                             // Nếu không chọn ảnh, bỏ qua toàn bộ khối này.

                const long maxSize = 2 * 1024 * 1024;                                     // Giới hạn dung lượng: 2 * 1024 * 1024 byte = 2 MB.

                string extension = Path.GetExtension(avatar.FileName).ToLowerInvariant(); // Lấy phần đuôi của tên file, ví dụ "anh.PNG" → ".png".
                                                                                          // ToLowerInvariant() giúp so sánh mà không phân biệt chữ hoa, chữ thường.

                if (avatar.Length > maxSize ||                                            // Nếu file quá 2 MB HOẶC đuôi file không nằm trong danh sách cho phép.
                    !new[] { ".jpg", ".jpeg", ".png", ".webp" }.Contains(extension))
                {
                    ViewBag.AllGenders = Enum.GetValues<Gender>().ToList();               // Khi trả lại form Create, phải tạo lại dữ liệu cho các nút chọn giới tính.
                            
                    ModelState.AddModelError(                                             // Gắn thông báo lỗi vào trường "avatar".
                        "avatar",
                        "Chọn ảnh JPG, PNG hoặc WEBP tối đa 2 MB.");

                    return View(s);                                                       // Hiện lại form Create với dữ liệu s đã nhập; không lưu sinh viên.
                }


                string folder = Path.Combine(   // Tạo đường dẫn THẬT trên máy chủ:
                    _env.WebRootPath,           // [đường dẫn wwwroot]/uploads/students
                    "uploads",
                    "students");

                
                Directory.CreateDirectory(folder);                                  // Tạo thư mục nếu chưa có. Nếu đã có thì không làm gì.

                
                
                string fileName = $"{Guid.NewGuid():N}{extension}";                // Tạo tên file mới, ví dụ "a3f...91.png".
                                                                                   // Guid giúp các ảnh ít bị trùng tên và không dùng tên file do người dùng gửi.

                string filePath = Path.Combine(folder, fileName);                  // Ghép thư mục và tên file thành vị trí lưu file trên máy chủ.

                using (var stream = new FileStream(filePath, FileMode.CreateNew))  // Tạo file mới tại filePath rồi chép nội dung ảnh tải lên vào đó.
                {
                    await avatar.CopyToAsync(stream);
                }                                                                  // Ra khỏi using, stream được đóng để giải phóng tài nguyên.


                s.AvatarUrl = $"/uploads/students/{fileName}";                     // Lưu URL vào sinh viên để sau này dùng trong <img src="...">.
            }

            // Phân biệt hai đường dẫn quan trọng: - filePath: nơi ảnh nằm trên ổ đĩa của server, dùng để lưu file.
            //                                     - s.AvatarUrl: địa chỉ trình duyệt truy cập, dùng để hiển thị ảnh.
            // Ví dụ file được lưu ở C:\DuAn\wwwroot\uploads\students\abc.png,
            // còn URL để hiển thị là               /uploads/students/abc.png.
            // Một điểm cần nhớ: đoạn kiểm tra.png, .jpg mới kiểm tra đuôi tên file -----> chưa xác minh nội dung bên trong thật sự là ảnh.

            //------------------------------: Xử lý upload ảnh :------------------------------: end

            // Chuẩn bị lại danh sách giới tính
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

            // Chuẩn bị lại danh sách ngành học

            ViewBag.AllBranches = new List<SelectListItem>()    // SelectListItem biểu diễn một lựa chọn trong ô chọn thả xuống:
            {
                new SelectListItem { Value = "1", Text = "IT" },// Text: chữ người dùng nhìn thấy.
                new SelectListItem { Value = "2", Text = "CE" },// Value: giá trị gửi về server khi chọn.
                new SelectListItem { Value = "3", Text = "BE" },
                new SelectListItem { Value = "4", Text = "EE" } // Tương ứng HTML:
            };                                                  // <option value="1">IT</option>


            if (ModelState.IsValid) // lab3
            {
                s.Id = Liststudents.Last().Id + 1;
                Liststudents.Add(s);
                return View("Index", Liststudents);
            }
            return View(s); // Nếu dữ liệu không hợp lệ, trả lại form Create với dữ liệu đã nhập và thông báo lỗi.
        }



    }
}
