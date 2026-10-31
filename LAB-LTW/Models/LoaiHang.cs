using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB_LTW.Models
{
    [Table("LoaiHang")]
    public class LoaiHang
    {
        [Key]
        [Display(Name = "Mã Loại")]
        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Tên loại không được để trống")]
        [StringLength(50)]
        [Display(Name = "Tên Loại")]
        public string TenLoai { get; set; } = string.Empty;

        public virtual ICollection<HangHoa> HangHoas { get; set; } = new List<HangHoa>();
    }
}
