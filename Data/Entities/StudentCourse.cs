using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class StudentCourse
    {
        [Key]
        public int Id { get; set; }
        public int StudentId{ get; set; }
        public int CourseId { get; set; }

        
    }
}
