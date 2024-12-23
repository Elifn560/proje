using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class Db : DbContext
    {
        public Db() : base(options: new DbContextOptionsBuilder<Db>().UseSqlServer("Data Source=DESKTOP-0ME848I;Initial Catalog=proje;Integrated Security=True;Encrypt=False").Options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<TeacherCourses> TeacherCourses { get; set; }
        public virtual DbSet<StudentCourse> StudentCourse{ get; set; }
      
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(s => s.EntrollmentDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Student>()
                .Property(s => s.EntrollmentDate)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Student>()
                .Property(s => s.isCourseSelectionConfirmed)
                .HasDefaultValueSql("0");


            base.OnModelCreating(modelBuilder);
        }
    }
}