using LAB_LTW.Models;
using Microsoft.AspNetCore.Mvc;

namespace LAB_LTW.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        LTWSchoolContext db;
        List<Major> majors;

        public MajorViewComponent(LTWSchoolContext _context)
        {
            db = _context;
            majors = db.Majors.ToList();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderMajor", majors);
        }
    }
}