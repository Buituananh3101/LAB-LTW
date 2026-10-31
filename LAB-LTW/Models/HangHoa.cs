using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB_LTW.Models
{
    // 2.1.4: Model HangHoa ánh xạ bảng HangHoa
    [Table("HangHoa")]
    public class HangHoa
    {
        [Key]
        public int MaHang { get; set; }
        public int MaLoai { get; set; }
        public string TenHang { get; set; }

        // 5.2.1: Giá là số trong khoảng 100 đến 5000
        [Range(100, 5000, ErrorMessage = "Giá phải là số từ 100 đến 5000")]
        public decimal? Gia { get; set; }

        // 5.2.2: Tên file ảnh phải có đuôi jpg, png, gif, tiff
        [RegularExpression(@"^.+\.(jpg|png|gif|tiff)$", ErrorMessage = "Tên ảnh phải có đuôi: .jpg | .png | .gif | .tiff")]
        public string Anh { get; set; }
    }
}
