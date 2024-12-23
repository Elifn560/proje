using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class StudentCourseSelection
    {
        [Key]
        public int SelectionId { get; set; } //pk
        public int StudentId { get; set; } // fk for students

        public virtual Student Student { get; set; }
        public int CourseId { get; set; } // fk for courses
        public virtual Course Course { get; set; }
        public DateTime SelectionDate { get; set; }= DateTime.Now;
    }
}
