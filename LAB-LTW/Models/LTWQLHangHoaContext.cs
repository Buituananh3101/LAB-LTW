using Microsoft.EntityFrameworkCore;

namespace LAB_LTW.Models
{
    // [2.1.5]: DbContext kết nối CSDL
    public class LTWQLHangHoaContext : DbContext
    {
        public LTWQLHangHoaContext(DbContextOptions<LTWQLHangHoaContext> opt) : base(opt) { }
        public DbSet<LoaiHang> LoaiHangs { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
    }
}
