using Microsoft.AspNetCore.Mvc;
using LAB_LTW.Models;

namespace LAB_LTW.Controllers
{
    public class StudentController : Controller
    {

        private List<Student> Liststudents;
        public StudentController() // Constructor khi tao List
        {
            Liststudents = new List<Student>()
            {
                new Student() { Id = 1, Name = "Nguyen Van A", Email = "nguyenvana@example.com", Password = "password1", Branch = Branch.IT, Gender = Gender.Male, IsRegular = true, Address = "123 Street, City", DateOfBorth = new DateTime(2000, 1, 1) },
                new Student() { Id = 2, Name = "Tran Thi B", Email = "tranthib@example.com", Password = "password2", Branch = Branch.BE, Gender = Gender.Female, IsRegular = false, Address = "456 Avenue, City", DateOfBorth = new DateTime(2001, 2, 2) },
                new Student() { Id = 3, Name = "Le Van C", Email = "", Password = "password3", Branch = Branch.CE, Gender = Gender.Male, IsRegular = true, Address = "789 Boulevard, City", DateOfBorth = new DateTime(2002, 3, 3) },
                new Student() { Id = 4, Name = "Pham Thi D", Email = "", Password = "password4", Branch = Branch.EE, Gender = Gender.Female, IsRegular = false, Address = "101 Road, City", DateOfBorth = new DateTime(2003, 4, 4) }
            };
        }

        public IActionResult Index()
        {
            return View(Liststudents);
        }
    }
}
