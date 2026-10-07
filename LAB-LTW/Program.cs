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

// branch base on: lab3/main

// 1. Tao 1 view MyLayout moi
// 2. copy file index thay cho vao 
// 3. keo css js vao
// 4. sua duong dan css js trong view vua tao
// 5. sua _ViewStart.cshtml de su dung view layout moi 

// 6. Dùng render section --> khi an vao list hien thi thi no chuyen sang mau vang :) --> chac chi de test code

// 7. Them Model MenuItem
// 8. Tao thu muc ViewComponents --> Tao class RenderViewComponents
// 9. Tao View duoc goi boi ViewComponent --> Tao thu muc Components --> Tao thu muc Render --> tao view RenderLeftMenu --> copy code tu LeftMenu sang RenderLeftMenu + them "@model = IEnumerable<MenuItem>" vao dau --> Render danh sach menuItem bang foreach
// 10. Tao view MyLayoutHelper --> copy code tu MyLayout sang MyLayoutHelper --> Thay <partial name="LeftMenu" /> ----> @await Component.InvokeAsync("Render")
