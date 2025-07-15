using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TestBooking> TestBookings { get; set; }
        public DbSet<TestBookingService> TestBookingServices { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<StaffSchedule> StaffSchedules { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<MenstrualCycle> MenstrualCycles { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.RoleId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Password).HasMaxLength(255).IsRequired();
                entity.Property(e => e.Gender).HasMaxLength(10);
                entity.Property(e => e.Email).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.PhoneNumber).IsUnique();

                entity.HasOne(e => e.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(e => e.RoleId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_User_Role");

                entity.HasOne(e => e.MedicalHistory)
                      .WithOne(m => m.User)
                      .HasForeignKey<MedicalHistory>(m => m.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_MedicalHistory_User");
            });

            // Role Configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(20).IsRequired();

                entity.HasData(
                    new Role
                    {
                        RoleId = Guid.Parse("cb923e1c-ed85-45a8-bc2f-8b78c60b7e28"),
                        Name = "Customer"
                    },
                    new Role
                    {
                        RoleId = Guid.Parse("d5cf10f1-1f31-4016-ac13-34667e9ca10d"),
                        Name = "Staff"
                    },
                    new Role
                    {
                        RoleId = Guid.Parse("157f0b62-afbb-44ce-91ce-397239875df5"),
                        Name = "Consultant"
                    },
                    new Role
                    {
                        RoleId = Guid.Parse("c5b82656-c6a7-49bd-a3fb-3d3e07022d33"),
                        Name = "Manager"
                    }
                );
            });

            // Service Configuration
            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");
                entity.HasKey(e => e.ServiceId);
                entity.Property(e => e.ServiceId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.ServiceName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)").IsRequired();
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                entity.HasData(
                    new Service
                    {
                        ServiceId = Guid.Parse("d220feba-eb1e-47d6-bc88-a044dcd45025"),
                        ServiceName = "HIV Test",
                        Description = "Blood test to detect HIV antibodies or antigens.",
                        Price = 50.00,
                        IsActive = true
                    },
                    new Service
                    {
                        ServiceId = Guid.Parse("92156da3-b20c-4b53-b0e4-748adaea4a75"),
                        ServiceName = "Chlamydia Test",
                        Description = "Urine or swab test to detect Chlamydia infection.",
                        Price = 40.00,
                        IsActive = true
                    },
                    new Service
                    {
                        ServiceId = Guid.Parse("c87031b9-f5ea-4494-a2f1-65743f194b8d"),
                        ServiceName = "Gonorrhea Test",
                        Description = "Swab or urine test to diagnose Gonorrhea.",
                        Price = 40.00,
                        IsActive = true
                    },
                    new Service
                    {
                        ServiceId = Guid.Parse("2bd04214-d426-49c1-b92c-061ca1057aa2"),
                        ServiceName = "Syphilis Test",
                        Description = "Blood test to detect Syphilis infection.",
                        Price = 45.00,
                        IsActive = true
                    }
                );
            });

            // MedicalHistory Configuration
            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.ToTable("MedicalHistory");
                entity.HasKey(e => e.MedicalHistoryId);
                entity.Property(e => e.MedicalHistoryId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasIndex(e => e.UserId).IsUnique();
            });

            // TestBooking Configuration
            modelBuilder.Entity<TestBooking>(entity =>
            {
                entity.ToTable("TestBooking");
                entity.HasKey(e => e.TestBookingId);
                entity.Property(e => e.TestBookingId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.MedicalHistoryId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.StaffId).HasMaxLength(10);
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.TestBookings)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_TestBooking_User");

                entity.HasOne(e => e.MedicalHistory)
                      .WithMany(m => m.TestBookings)
                      .HasForeignKey(e => e.MedicalHistoryId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_TestBooking_MedicalHistory");

                entity.HasOne(e => e.BookingStaff)
                      .WithMany(u => u.StaffTestBookings)
                      .HasForeignKey(e => e.StaffId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_TestBooking_Staff");
            });

            // TestBookingService Configuration
            modelBuilder.Entity<TestBookingService>(entity =>
            {
                entity.ToTable("TestBookingService");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasMaxLength(10).IsRequired();
                entity.Property(e => e.TestBookingId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.ServiceId).HasMaxLength(10).IsRequired();

                entity.HasOne(e => e.TestBooking)
                      .WithMany(tb => tb.TestBookingServices)
                      .HasForeignKey(e => e.TestBookingId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_TestBookingService_TestBooking");

                entity.HasOne(e => e.Service)
                      .WithMany(s => s.TestBookingServices)
                      .HasForeignKey(e => e.ServiceId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_TestBookingService_Service");
            });

            // TestResult Configuration
            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.ToTable("TestResult");
                entity.HasKey(e => e.TestResultId);
                entity.Property(e => e.TestResultId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.TestBookingServiceId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.TestName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ResultDetail).HasMaxLength(500);
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();

                entity.HasOne(e => e.TestBookingService)
                      .WithMany(tbs => tbs.TestResults)
                      .HasForeignKey(e => e.TestBookingServiceId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_TestResult_TestBookingService");
            });

            // Appointment Configuration
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");
                entity.HasKey(e => e.AppointmentId);
                entity.Property(e => e.AppointmentId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.ConsultantId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.MeetingUrl).HasMaxLength(255);
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.PatientAppointments)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Appointment_User");

                entity.HasOne(e => e.Consultant)
                      .WithMany(u => u.ConsultingAppointments)
                      .HasForeignKey(e => e.ConsultantId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Appointment_Consultant");
            });

            // StaffSchedule Configuration
            modelBuilder.Entity<StaffSchedule>(entity =>
            {
                entity.ToTable("StaffSchedule");
                entity.HasKey(e => e.StaffScheduleId);
                entity.Property(e => e.StaffScheduleId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.ConsultantId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Status).HasMaxLength(20).IsRequired();

                entity.HasOne(e => e.Consultant)
                      .WithMany(u => u.StaffSchedules)
                      .HasForeignKey(e => e.ConsultantId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_StaffSchedule_Consultant");
            });

            // Blog Configuration
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");
                entity.HasKey(e => e.BlogId);
                entity.Property(e => e.BlogId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Tittle).HasMaxLength(100).IsRequired();
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Blogs)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Blog_User");
            });

            // Feedback Configuration
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");
                entity.HasKey(e => e.FeedbackId);
                entity.Property(e => e.FeedbackId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.ServiceId).HasMaxLength(10);
                entity.Property(e => e.Title).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Content).HasMaxLength(500);
                entity.Property(e => e.Rating).IsRequired().HasConversion<int>()
                    .HasAnnotation("CheckConstraint", "Rating >= 1 AND Rating <= 5");
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Feedbacks)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Feedback_User");

                entity.HasOne(e => e.Service)
                      .WithMany(s => s.Feedbacks)
                      .HasForeignKey(e => e.ServiceId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_Feedback_Service");
            });

            // MenstrualCycle Configuration
            modelBuilder.Entity<MenstrualCycle>(entity =>
            {
                entity.ToTable("MenstrualCycle");
                entity.HasKey(e => e.MenstrualCycleId);
                entity.Property(e => e.MenstrualCycleId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.Note).HasMaxLength(500);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.MenstrualCycles)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_MenstrualCycle_User");
            });

            // Question Configuration
            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");
                entity.HasKey(e => e.QuestionId);
                entity.Property(e => e.QuestionId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(10).IsRequired();
                entity.Property(e => e.ConsultantId).HasMaxLength(10);
                entity.Property(e => e.Title).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Content).HasMaxLength(500).IsRequired();
                entity.Property(e => e.Answer).HasMaxLength(500);
                entity.Property(e => e.IsActive).HasDefaultValue(true);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Questions)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Question_User");

                entity.HasOne(e => e.Consultant)
                      .WithMany(u => u.AnsweredQuestions)
                      .HasForeignKey(e => e.ConsultantId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_Question_Consultant");
            });
        }
    }
}