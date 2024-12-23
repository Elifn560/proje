using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;



namespace proje.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            using (Db db = new Db())
            {
                List<Student> allStudent = db.Student.ToList();
                return PartialView(allStudent);
            }
        }

        public IActionResult Transcript()
        {
            using (Db db = new Db())
            {
                int userId = Convert.ToInt32(HttpContext?.Session?.GetInt32("CurrentUserId"));
                Student currentStudent = db.Student.Find(userId);

                List<StudentCourse> studentCoursesIds = db.StudentCourse.Where(x => x.StudentId == currentStudent.StudentId).ToList();
                List<dynamic> studentCourses = new List<dynamic>();

                if (currentStudent.isCourseSelectionConfirmed)
                {
                    foreach (StudentCourse studentCourseId in studentCoursesIds)
                    {
                        Course studentCourse = db.Course.Find(studentCourseId.CourseId);
                        if (studentCourse != null)
                        {
                            Teacher courseTeacher = db.Teacher.Find(studentCourse.CourseId);
                            if (courseTeacher != null)
                            {
                                studentCourses.Add(new
                                {
                                    CourseId = studentCourseId.CourseId,
                                    CourseName = studentCourse.CourseName,
                                    CourseCredit = studentCourse.CourseCredit,
                                    CourseType = studentCourse.CourseType,
                                    TeacherName = courseTeacher.Name + " " + courseTeacher.Surname

                                });

                            }
                        }
                    }
                }
                return PartialView(studentCourses);
            }
        }


        public IActionResult CourseSelection()
        {
            using (Db db = new Db()) {

                int userId = Convert.ToInt32(HttpContext?.Session?.GetInt32("CurrentUserId"));
                Student currentStudent = db.Student.Find(userId);
                List<StudentCourse> studentCourseIdList = db.StudentCourse.Where(x => x.StudentId == userId).ToList();
                List<Course> allCourse = db.Course.ToList();
                List<dynamic> allCourseWithTeacherName = new List<dynamic>();

                foreach (Course course in allCourse)
                {
                    TeacherCourses teacherCourse = db.TeacherCourses.Where(x => x.CourseId == course.CourseId).FirstOrDefault();

                    if (teacherCourse != null)
                    {
                        Teacher courseTeacher = db.Teacher.Find(teacherCourse.TeacherId);
                        if (courseTeacher != null)
                        {
                            if (!studentCourseIdList.Any(x => x.CourseId == course.CourseId))
                            {
                                allCourseWithTeacherName.Add(new
                                {
                                    CourseId = course.CourseId,
                                    CourseName = course.CourseName,
                                    CourseCredit = course.CourseCredit,
                                    CourseType = course.CourseType,
                                    TeacherName = courseTeacher.Name + " " + courseTeacher.Surname
                                });
                            }
                        }

                    }
                }


                List<dynamic> studentCourse = new List<dynamic>();
                foreach (StudentCourse courseId in studentCourseIdList)
                {
                    Course course = db.Course.Find(courseId.CourseId);
                    if (course != null)
                    {
                        TeacherCourses teacherId = db.TeacherCourses.Where(x => x.CourseId == course.CourseId).FirstOrDefault();
                        if (teacherId != null)
                        {
                            Teacher courseTeacher = db.Teacher.Find(teacherId.TeacherId);
                            studentCourse.Add(new
                            {
                                CourseId = courseId.CourseId,
                                CourseName = course.CourseName,
                                CourseCredit = course.CourseCredit,
                                CourseType = course.CourseType,
                                TeacherName = courseTeacher.Name + " " + courseTeacher.Surname
                            });
                        }
                    }
                }
                return PartialView(new
                {
                    AllCourses = allCourseWithTeacherName,
                    StudentCourses = studentCourse,
                    isConfirmed = currentStudent.isCourseSelectionConfirmed
                });
            }
        }
    


        public IActionResult SelectCourse(string SelectedCoursesIds)
        {
            List<int> selectedCourseIds = JsonSerializer.Deserialize<List<int>>(SelectedCoursesIds);
            int currentStudentId = Convert.ToInt32(HttpContext?.Session?.GetInt32("CurrentUserId"));

            using (Db db = new Db())
            {
                List<Course> requiredCourses = db.Course.Where(x => x.CourseType == true).ToList();
                List<TeacherCourses> allCourseIdsWithTeacher = db.TeacherCourses.ToList();

                for (int i = requiredCourses.Count - 1; i >= 0; i--)
                {
                    Course requiredCourse = requiredCourses[i];

                    if (!allCourseIdsWithTeacher.Any(x => x.CourseId == requiredCourse.CourseId))
                    {
                        requiredCourses.RemoveAt(i);
                    }
                }

                bool allReaquiredCourseSelected = true;

                foreach (Course requiredCourse in requiredCourses)
                {
                    if (!selectedCourseIds.Contains(requiredCourse.CourseId))
                    {
                        allReaquiredCourseSelected = false;
                        break;
                    }
                }


                if (!allReaquiredCourseSelected)
                {
                    return Json(new { success = false, message = "Bütün zorunlu dersler seçilmelidir!" });
                }

                if (!allReaquiredCourseSelected)
                {
                    foreach (int courseId in SelectedCoursesIds)
                    {
                        Course selectedCourse = db.Course.Find(courseId);
                        if (selectedCourse != null)
                        {
                            StudentCourse studentCourse = new StudentCourse
                            {
                                CourseId = selectedCourse.CourseId,
                                StudentId = currentStudentId
                            };

                            db.StudentCourse.Add(studentCourse);
                        }
                    }

                    db.SaveChanges();
                }
                return Json(new { success = true, message = "Başarıyla kayıt edildi!" });
            }
        }
    }
}