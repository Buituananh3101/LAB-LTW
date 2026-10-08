var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

// Packet

// Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore 
// Microsoft.EntityFrameworkCore.SqlServer 
// Microsoft.EntityFrameworkCore.Design

// Scaffold-DbContext [-Connection] [-Provider] [-OutputDir] [-Context] [-Schemas>] [-Tables>] [-DataAnnotations] [-Force] [-Project] [-StartupProject] [<CommonParameters>]

// Scaffold-DbContext "Data Source=(local);Initial Catalog=School;Persist Security Info=True;User ID=sa;Password=123123;" Microsoft.EntityFrameworkCore.SqlServer -Project DBFirstApp -OutputDir Models