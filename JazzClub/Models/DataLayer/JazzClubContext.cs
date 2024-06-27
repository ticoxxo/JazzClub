using Microsoft.EntityFrameworkCore;
using JazzClub.Models.Configuration;
using JazzClub.Models.DomainModels;

namespace JazzClub.Models.DataLayer
{
    public class JazzClubContext : DbContext
    {
        
        public JazzClubContext(DbContextOptions<JazzClubContext> options) 
            : base(options)
            { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;

        public DbSet<Fingertip> Fingertips { get; set; } = null!;

        public DbSet<Guardian> Guardians { get; set; } = null!;

        public DbSet<Student> Students { get; set; } = null!;

        public DbSet<CourseStudent> CoursesStudents { get; set; } = null!;

        public DbSet<Day> Days { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new CourseConfig());
            modelBuilder.ApplyConfiguration(new FingerprintConfig());
            modelBuilder.ApplyConfiguration(new GuardianConfig());
            modelBuilder.ApplyConfiguration(new StudentConfig());
            modelBuilder.ApplyConfiguration(new CoursesStudentsConfig());
            modelBuilder.ApplyConfiguration(new DayConfigf());
        }
    }
}
