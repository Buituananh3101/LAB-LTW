using Microsoft.EntityFrameworkCore;

namespace LAB_LTW.Models
{
    public class LTWQLHangHoaContext : DbContext
    {
        public LTWQLHangHoaContext(DbContextOptions<LTWQLHangHoaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LoaiHang> LoaiHangs { get; set; }
        public virtual DbSet<HangHoa> HangHoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoaiHang>(entity =>
            {
                entity.ToTable("LoaiHang");
                entity.HasKey(e => e.MaLoai);
            });

            modelBuilder.Entity<HangHoa>(entity =>
            {
                entity.ToTable("HangHoa");
                entity.HasKey(e => e.MaHang);

                entity.HasOne(d => d.LoaiHang)
                    .WithMany(p => p.HangHoas)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HangHoa_LoaiHang");
            });
        }
    }
}
