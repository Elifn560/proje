using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Context;

namespace proje.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            using (Db db= new Db())
            {
                List<Course> allCourses = db.Course.ToList();
                return PartialView(allCourses);
            }
           
        }




    }
}



