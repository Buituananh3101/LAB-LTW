using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB_LTW.Models
{
    // [2.1.3]: Model LoaiHang ánh xạ bảng LoaiHang
    [Table("LoaiHang")]
    public class LoaiHang
    {
        [Key]
        public int MaLoai { get; set; }
        public string TenLoai { get; set; }
    }
}
