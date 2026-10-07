using System.ComponentModel.DataAnnotations;

namespace LAB_LTW.Models
{
    //public class Student
    //{
    //    public int Id { get; set; }              // Mã sinh viên
    //    public string? Name { get; set; }        // Họ tên       // ? la cho phep null
    //    public string? Email { get; set; }       // Email
    //    public string? Password { get; set; }    // Mật khẩu
    //    public Branch? Branch { get; set; }      // Ngành học
    //    public Gender? Gender { get; set; }      // Giới tính
    //    public bool IsRegular { get; set; }     // true: chính quy; false: phi chính quy
    //    public string? Address { get; set; }     // Địa chỉ
    //    public DateTime DateOfBorth { get; set; } // Ngày sinh

    //    public string? AvatarUrl { get; set; }
    //}

    public class Student
    {
        public int Id { get; set; }//Mã sinh viên

        // [Required]: Đánh dấu thuộc tính này là bắt buộc, không được để trống (null hoặc chuỗi rỗng).
        [Required]
        public string? Name { get; set; } //Họ tên

        // [Required(ErrorMessage = "...")] Bắt buộc nhập, nếu vi phạm sẽ hiển thị câu thông báo lỗi tùy chỉnh này thay vì thông báo mặc định của hệ thống.
        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        // [RegularExpression]: Ràng buộc dữ liệu phải khớp với khuôn mẫu biểu thức chính quy (Regex).
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string? Email { get; set; } //Email

        // [StringLength]: Quy định độ dài của chuỗi. Tham số đầu tiên (100) là độ dài tối đa, thuộc tính MinimumLength (8) là độ dài tối thiểu.
        [StringLength(100, MinimumLength = 8)]
        [Required]
        public string? Password { get; set; }//Mật khẩu

        [Required]
        public Branch? Branch { get; set; }//Ngành học

        [Required]
        public Gender? Gender { get; set; }//Giới tính

        public bool IsRegular { get; set; }//Hệ: true-chính quy, false-phi chính quy

        // [DataType]: Chỉ định loại dữ liệu cụ thể để UI biết cách hiển thị. DataType.MultilineText sẽ giúp Razor view tự động tạo thẻ <textarea> thay vì <input type="text"> mặc định.
        [DataType(DataType.MultilineText)]
        [Required]
        public string? Address { get; set; }//Địa chỉ

        // [Range]: Giới hạn giá trị phải nằm trong một khoảng cho trước. Ở đây, yêu cầu kiểu dữ liệu DateTime với ngày nhỏ nhất là 1/1/1963 và lớn nhất là 12/31/2005.
        [Range(typeof(DateTime), "1963-01-01", "2005-12-31")]
        //[Range(typeof(DateTime), "1/1/1963", "12/31/2005")]
        // [DataType.Date]: Chỉ định dữ liệu chỉ lấy Ngày (không lấy Giờ), giúp UI render ra bộ chọn lịch (Date picker) `<input type="date">`.
        [DataType(DataType.Date)]
        [Required]
        public DateTime DateOfBorth { get; set; }//Ngày sinh (Lưu ý: "DateOfBorth" có thể là lỗi đánh máy, thường viết là "DateOfBirth")

        public string? AvatarUrl { get; set; }
    }
}