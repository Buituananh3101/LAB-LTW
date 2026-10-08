using Microsoft.EntityFrameworkCore;

namespace LAB_LTW.Models
{
    public class LTWSchoolContext : DbContext                                   // cầu nối trực tiếp giữa mã nguồn C# và cơ sở dữ liệu SQL Server.
    {
        public LTWSchoolContext(DbContextOptions<LTWSchoolContext> options)     // nhận strConnect từ Program.cs via tham số DbContextOptions
            : base(options) { }
        public virtual DbSet<Course> Courses { get; set; }                      // DbSet<T> Đại diện cho các bảng dữ liệu T
        public virtual DbSet<Learner> Learners { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Major> Majors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)      // ghi đè hoặc bổ sung các quy tắc tạo CSDL mặc định của EF Core
        {
            modelBuilder.Entity<Major>().ToTable(nameof(Major));                // ToTable(nameof(...)) ép hệ thống đặt tên bảng theo dạng số ít
            modelBuilder.Entity<Course>().ToTable(nameof(Course));
            modelBuilder.Entity<Learner>().ToTable(nameof(Learner));
            modelBuilder.Entity<Enrollment>().ToTable(nameof(Enrollment));
        }
    }
}
