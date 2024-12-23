using Microsoft.AspNetCore.Mvc;
using Data.Context;
using Data.Entities;


namespace proje.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Entering(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password)) {
                using (Db db = new Db())
                {
                    List<Admin> isAdmin = db.Admin.Where(x => x.Email == email && x.Password == password).ToList();
                    List<Teacher> isTeacher = db.Teacher.Where(x => x.Email == email && x.Password == password).ToList();
                    List<Student> isStudent = db.Student.Where(x => x.Email == email && x.Password == password).ToList();

                    if (isAdmin.Count > 0)
                    {
                        Admin currentUser = isAdmin[0];
                        HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);
                        HttpContext?.Session?.SetString("Job", currentUser.Role);
                        HttpContext?.Session?.SetInt32("CurrentUserId", currentUser.Id);
                        HttpContext?.Session?.SetString("CurrentRole", "Admin");

                        return RedirectToAction("Index", "Home");
                    }
                    else if (isStudent.Count > 0) 
                    {
                        Student currentUser = isStudent[0];
                        HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);
                        HttpContext?.Session?.SetString("Job", currentUser.Role);
                        HttpContext?.Session?.SetInt32("CurrentUserId", currentUser.StudentId);
                        HttpContext?.Session?.SetString("CurrentRole", "Student");

                        return RedirectToAction("Index", "Home");
                    }

                    else if (isTeacher.Count > 0)
                    {
                        Teacher currentUser = isTeacher[0];
                        HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);
                        HttpContext?.Session?.SetString("Job", currentUser.Role);
                        HttpContext?.Session?.SetInt32("CurrentUserId", currentUser.TeacherId);
                        HttpContext?.Session?.SetString("CurrentRole", "Student");

                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            return RedirectToAction("Index", "Login");
        }


        public IActionResult Logout()
        {
            HttpContext?.Session?.Clear();
            return Json(new { redirectUrl = Url.Action("Index", "Login") });
        }
    }
}

