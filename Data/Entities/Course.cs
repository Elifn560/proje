using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
       
        public string CourseCredit { get; set; }

        public string CourseName { get; set; }
        public bool CourseType { get; set; }
       
    }
}
