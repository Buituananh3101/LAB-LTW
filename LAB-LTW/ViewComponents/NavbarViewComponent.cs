using LAB_LTW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB_LTW.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        private readonly LTWQLHangHoaContext _context;

        public NavbarViewComponent(LTWQLHangHoaContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loaiHangs = await _context.LoaiHangs.ToListAsync();
            return View(loaiHangs);
        }
    }
}
