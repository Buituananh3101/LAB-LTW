using LAB_LTW.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// 2.1.2: Đăng ký LTWQLHangHoaContext vào DI container (Models/LoaiHang.cs - 6)
builder.Services.AddDbContext<LTWQLHangHoaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LTWQLHangHoaContext")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
