using JazzClub.Model.DomainsModels;
using JazzClub.Models.Configuration;
using JazzClub.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace JazzClub.Models.DataLayer
{
	 public class JazzClubContext: IdentityDbContext<ApplicationUser>
    { 

		public JazzClubContext(DbContextOptions<JazzClubContext> options) : base(options) 
		{
		}

		//public DbSet<User> Users { get; set; } = null!;
		public DbSet<Course> Courses { get; set; } = null!;

		public DbSet<Fingertip> Fingertips { get; set; } = null!;


		public DbSet<Student> Students { get; set; } = null!;

		public DbSet<CourseStudent> CoursesStudents { get; set; } = null!;
        public DbSet<CourseDay> CourseDays { get; set; } = null!;

        public DbSet<Day> Days { get; set; } = null!;

		public DbSet<Payment> Payments { get; set; } = null!;


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = "c25553fe-1a0f-4f68-aec3-0aa8679b3778",
                UserName = "vera@jazzclub.com",
                NormalizedUserName = "VERA@JAZZCLUB.COM",
                Email = "vera@jazzclub.com",
                NormalizedEmail = "VERA@JAZZCLUB.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "jazzAdmin.1!"),
                SecurityStamp = "Z2MEC6PRRVGTMU4LUR7GUJ3RFVOVQIUY",
                ConcurrencyStamp = "a8efd5ec-c350-41ac-b64f-b789068a3bfc",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0

            });

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
