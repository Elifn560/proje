using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace proje.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            using (Db db = new Db())
            {
                List<Teacher> allTeacher = db.Teacher.ToList();
                return PartialView(allTeacher);
            }
        }

        public IActionResult TeacherDetay(int Id)
        {
            using (Db db = new Db())
            {
                Teacher teacher = db.Teacher.Find(Id);
                List<TeacherCourses> teacherCourseIds = db.TeacherCourses.Where(x => x.TeacherId == Id).ToList();

                List<Course> teacherCourse = new List<Course>();

                foreach (TeacherCourses teacherCourseId in teacherCourseIds)
                {
                    Course course = db.Course.Find(teacherCourseId.CourseId);
                    if (course != null)
                    {
                        teacherCourse.Add(course);
                    }
                }
                return PartialView(new
                {
                    TeacherInfo = teacher,
                    Course = teacherCourse
                });
            }
        }

        public IActionResult StudentCourseSelectionConfirmPage()
        {
            using (Db db = new Db())
            {
                string sessionId = HttpContext?.Session?.GetInt32("CurrentUserId").ToString();
                int userId = Convert.ToInt32(sessionId);

                Teacher teacher = db.Teacher.Find(userId);
                List<Student> isIntructor = db.Student.Where(s => s.InstructorId == teacher.TeacherId && s.isCourseSelectionConfirmed == false).ToList();

                return PartialView(isIntructor);
            }

        }

        [HttpPost]

        public IActionResult StudentCourseConfirm(int Id)
        {
            using (Db db = new Db())
            {
                Student confirmedStudent = db.Student.Find(Id);
                if (confirmedStudent != null)
                {
                    StudentCourse confirmedCourse = db.StudentCourse.Where(s => s.StudentId == confirmedStudent.StudentId).FirstOrDefault();

                    confirmedStudent.isCourseSelectionConfirmed = true;

                    db.SaveChanges();
                    return Json(new { success = true, message = "Öğrenci Ders Seçimi Onaylandı!" });
                }
            }
            return Json(new { success = false, message = "Bir Sorun Oluştu!" });
        }


        [HttpPost]

        public IActionResult StudentCourseReject(int Id)
        {
            using (Db db = new Db())
            {
                Student confirmedStudent = db.Student.Find(Id);
                if (confirmedStudent != null)
                {
                    List<StudentCourse> confirmedCourse = db.StudentCourse.Where(s => s.StudentId == confirmedStudent.StudentId).ToList();
                    db.StudentCourse.RemoveRange(confirmedCourse);

                    db.SaveChanges();
                    return Json(new { success = true, message = "Öğrenci Ders Seçimi Reddedildi!" });
                }
            }
            return Json(new { success = false, message = "Bir Sorun Oluştu!" });
        }
    }
}
