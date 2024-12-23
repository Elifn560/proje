using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class TeacherCourses
    {
        [Key]
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
    }
}
