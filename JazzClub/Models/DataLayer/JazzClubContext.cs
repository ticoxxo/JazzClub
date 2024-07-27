using JazzClub.Model.DomainsModels;
using JazzClub.Models.Configuration;
using JazzClub.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JazzClub.Models.DataLayer
{
	 public class JazzClubContext: DbContext
	{ 

		public JazzClubContext(DbContextOptions<JazzClubContext> options) : base(options) 
		{
		}

		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Course> Courses { get; set; } = null!;

		public DbSet<Fingertip> Fingertips { get; set; } = null!;


		public DbSet<Student> Students { get; set; } = null!;

		public DbSet<CourseStudent> CoursesStudents { get; set; } = null!;
        public DbSet<CourseDay> CourseDays { get; set; } = null!;

        public DbSet<Day> Days { get; set; } = null!;

		public DbSet<Payment> Payments { get; set; } = null!;


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfig());
			modelBuilder.ApplyConfiguration(new CourseConfig());
			modelBuilder.ApplyConfiguration(new FingerprintConfig());
			modelBuilder.ApplyConfiguration(new StudentConfig());
			modelBuilder.ApplyConfiguration(new CoursesStudentsConfig());
			modelBuilder.ApplyConfiguration(new DayConfig());
            modelBuilder.ApplyConfiguration(new CoursesDaysConfig());
			modelBuilder.ApplyConfiguration(new PaymentConfig());
        }

    }
}
