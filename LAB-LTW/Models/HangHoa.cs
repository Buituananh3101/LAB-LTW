using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB_LTW.Models
{
    // [2.1.4]: Xây dựng Model HangHoa ánh xạ tới bảng HangHoa trong CSDL
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        [Display(Name = "Mã Hàng")]
        public int MaHang { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại hàng")]
        [Display(Name = "Loại Hàng")]
        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Tên hàng không được để trống")]
        [StringLength(50, ErrorMessage = "Tên hàng tối đa 50 ký tự")]
        [Display(Name = "Tên Hàng")]
        public string TenHang { get; set; } = string.Empty;

        // [5.2.1]: Validation Annotation - Giá phải là số và nằm trong giới hạn từ 100 đến 5000
        [Required(ErrorMessage = "Giá không được để trống")]
        [Range(100, 5000, ErrorMessage = "Giá phải là số và nằm trong giới hạn từ 100 đến 5000")]
        [Column(TypeName = "numeric(18, 0)")]
        [Display(Name = "Giá")]
        public decimal? Gia { get; set; }

        // [5.2.2]: Validation Annotation - Tên file ảnh phải có đuôi: .jpg | .png | .gif | .tiff
        [Required(ErrorMessage = "Tên file ảnh không được để trống")]
        [StringLength(100)]
        [RegularExpression(@"^.+\.(jpg|png|gif|tiff)$", ErrorMessage = "Tên file ảnh phải có đuôi: .jpg | .png | .gif | .tiff")]
        [Display(Name = "Ảnh")]
        public string? Anh { get; set; }

        // [2.1.4]: Khóa ngoại liên kết tới bảng LoaiHang
        [ForeignKey("MaLoai")]
        public virtual LoaiHang? LoaiHang { get; set; }
    }
}
