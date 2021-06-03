using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARM.Models;
using Microsoft.EntityFrameworkCore;

namespace ARM.Data
{
    public class ARMDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Speciality> Specialities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ARM;Persist Security Info=True;User ID=admin;Password=admin");
            //optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users", "dbo");
            modelBuilder.Entity<User>().Property(p => p.Id).HasColumnName("user_id");
            modelBuilder.Entity<User>().Property(p => p.Name).HasColumnName("user_name");
            modelBuilder.Entity<User>().Property(p => p.FirstName).HasColumnName("user_firstname");
            modelBuilder.Entity<User>().Property(p => p.LastName).HasColumnName("user_lastname");
            modelBuilder.Entity<User>().Property(p => p.MiddleName).HasColumnName("user_middlename");
            modelBuilder.Entity<User>().Property(p => p.Password).HasColumnName("user_password");

            modelBuilder.Entity<Student>().ToTable("student", "dbo");
            modelBuilder.Entity<Student>().Property(p => p.Id).HasColumnName("student_id");
            modelBuilder.Entity<Student>().Property(p => p.Name).HasColumnName("student_name");
            modelBuilder.Entity<Student>().Property(p => p.LastName).HasColumnName("student_lastname");
            modelBuilder.Entity<Student>().Property(p => p.MiddleName).HasColumnName("student_middlename");
            modelBuilder.Entity<Student>().Property(p => p.PhoneNumber).HasColumnName("student_phone_number");
            modelBuilder.Entity<Student>().Property(p => p.IsGroupHead).HasColumnName("student_is_group_head");
            modelBuilder.Entity<Student>().Property(p => p.GroupId).HasColumnName("group_id");
            modelBuilder.Entity<Student>().HasOne(p => p.Group).WithMany(p => p.Students).HasForeignKey(p => p.GroupId);

            modelBuilder.Entity<Group>().ToTable("group", "dbo");
            modelBuilder.Entity<Group>().Property(p => p.Id).HasColumnName("group_id");
            modelBuilder.Entity<Group>().Property(p => p.Name).HasColumnName("group_name");
            modelBuilder.Entity<Group>().Property(p => p.Title).HasColumnName("group_title");
            modelBuilder.Entity<Group>().Property(p => p.SpecialityId).HasColumnName("speciality_id");
            modelBuilder.Entity<Group>().Property(p => p.DateBegin).HasColumnName("group_date_begin");
            modelBuilder.Entity<Group>().HasOne(p => p.Speciality).WithMany(p => p.Groups)
                .HasForeignKey(p => p.SpecialityId);

            modelBuilder.Entity<Speciality>().ToTable("speciality", "dbo");
            modelBuilder.Entity<Speciality>().Property(p => p.Id).HasColumnName("speciality_id");
            modelBuilder.Entity<Speciality>().Property(p => p.Name).HasColumnName("speciality_name");
            modelBuilder.Entity<Speciality>().Property(p => p.Title).HasColumnName("speciality_title");
            modelBuilder.Entity<Speciality>().Property(p => p.DepartmentId).HasColumnName("department_id");
            modelBuilder.Entity<Speciality>().Property(p => p.Cost).HasColumnName("speciality_cost");
            modelBuilder.Entity<Speciality>().Property(p => p.IsActual).HasColumnName("speciality_is_actual");
            modelBuilder.Entity<Speciality>().Property(p => p.LearnMonth).HasColumnName("speciality_learn_months");
            modelBuilder.Entity<Speciality>().HasOne(p => p.Department).WithMany(p => p.Specialities)
                .HasForeignKey(p => p.DepartmentId);

            modelBuilder.Entity<Faculty>().ToTable("faculty", "dbo");
            modelBuilder.Entity<Faculty>().Property(p => p.Id).HasColumnName("faculty_id");
            modelBuilder.Entity<Faculty>().Property(p => p.Name).HasColumnName("faculty_name");
            modelBuilder.Entity<Faculty>().Property(p => p.Title).HasColumnName("faculty_title");
            modelBuilder.Entity<Faculty>().Property(p => p.FacultyDecaneId).HasColumnName("faculty_decane_id");
            modelBuilder.Entity<Faculty>().HasOne(p => p.FacultyDecane).WithOne()
                .HasForeignKey<Faculty>(p => p.FacultyDecaneId);

            modelBuilder.Entity<Department>().ToTable("department", "dbo");
            modelBuilder.Entity<Department>().Property(p => p.Id).HasColumnName("department_id");
            modelBuilder.Entity<Department>().Property(p => p.Name).HasColumnName("department_name");
            modelBuilder.Entity<Department>().Property(p => p.Title).HasColumnName("department_title");
            modelBuilder.Entity<Department>().Property(p => p.FacultyId).HasColumnName("faculty_id");
            modelBuilder.Entity<Department>().HasOne(p => p.Faculty).WithMany(p => p.Departments)
                .HasForeignKey(p => p.FacultyId);
        }
    }
}
