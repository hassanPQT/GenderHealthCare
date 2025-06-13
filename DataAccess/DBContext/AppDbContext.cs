using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<StaffSchedule> StaffSchedules { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<MenstrualCycle> MenstrualCycles { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();

                entity.HasOne(e => e.Role)
                      .WithMany(u => u.Users)
                      .HasForeignKey(e => e.RoleId)
                      .HasConstraintName("FK_User_Role");

                entity.HasMany(e => e.Blogs)
                      .WithOne(b => b.User)
                      .HasForeignKey(b => b.UserId)
                      .HasConstraintName("FK_Blog_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");
                entity.HasKey(e => e.RoleId);
                entity.Property(e => e.RoleId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();

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

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");
                entity.HasKey(e => e.ServiceId);
                entity.Property(e => e.ServiceName).IsRequired();
                entity.Property(e => e.ServiceId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();

                entity.HasMany(s => s.TestResults)
                      .WithOne(tr => tr.Service)
                      .HasForeignKey(tr => tr.ServiceId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_TestResult_Service");

                entity.HasMany(s => s.TestBookings)
                      .WithOne(tb => tb.Service)
                      .HasForeignKey(tb => tb.ServiceId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_TestBooking_Service");

                // Seed data for Service table
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

            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.ToTable("MedicalHistory");
                entity.HasKey(e => new { e.MedicalHistoryId });

                entity.HasOne(e => e.User)
                      .WithMany(u => u.MedicalHistories)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_MedicalHistory_User");

                entity.HasMany(mh => mh.TestBookings)
                      .WithOne(tb => tb.MedicalHistory)
                      .HasForeignKey(tb => tb.MedicalHistoryId)
                      .HasConstraintName("FK_TestBooking_MedicalHistory");


            });

            modelBuilder.Entity<TestResult>(entity =>
            {
                entity.ToTable("TestResult");
                entity.HasKey(e => e.TestResultId);
                entity.Property(e => e.TestResultId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.TestResults)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_TestResult_User");
            });

            modelBuilder.Entity<TestBooking>(entity =>
            {
                entity.ToTable("TestBooking");
                entity.HasKey(e => e.TestBookingId);
                entity.Property(e => e.TestBookingId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.TestBookings)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_TestBooking_User");

                entity.HasOne(e => e.Staff)
                    .WithMany(u => u.HandledTestBookings)
                    .HasForeignKey(e => e.StaffId)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_TestBooking_Staff");

                entity.HasMany(tb => tb.TestResults)
                      .WithOne(tr => tr.TestBooking)
                      .HasForeignKey(tr => tr.TestBookingId)
                      .HasConstraintName("FK_TestResult_TestBooking");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");
                entity.HasKey(e => e.AppointmentId);
                entity.Property(e => e.AppointmentId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Appointments)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_Appointment_User");

                entity.HasOne(e => e.Consultant)
                       .WithMany(u => u.ConsultingAppointments)
                       .HasForeignKey(e => e.ConsultantId)
                       .OnDelete(DeleteBehavior.NoAction)
                       .HasConstraintName("FK_Appointment_Consultant");

                entity.HasOne(e => e.StaffSchedule)
                      .WithMany(u => u.Appointments)
                      .HasForeignKey(e => e.StaffScheduleId)
                      .HasConstraintName("FK_Appointment_StaffSchedule");
            });

            modelBuilder.Entity<StaffSchedule>(entity =>
            {
                entity.ToTable("StaffSchedule");
                entity.HasKey(e => e.StaffScheduleId);
                entity.Property(e => e.StaffScheduleId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.StaffSchedules)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_StaffSchedule_Consultant");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");
                entity.HasKey(e => e.BlogId);
                entity.Property(e => e.BlogId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
                entity.Property(e => e.Tittle).IsRequired();
                entity.Property(e => e.Content).IsRequired();
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Blogs)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_Blog_User");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");
                entity.HasKey(e => e.FeedbackId);
                entity.Property(e => e.FeedbackId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Feedbacks)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_Feedback_User");

                entity.HasOne(f => f.Service)
                      .WithMany(s => s.Feedbacks)
                      .HasForeignKey(f => f.ServiceId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_Feedback_Service");
            });

            modelBuilder.Entity<MenstrualCycle>(entity =>
            {
                entity.ToTable("MenstrualCycle");
                entity.HasKey(e => e.MenstrualCycleId);
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.MenstrualCycleId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.MenstrualCycles)
                      .HasForeignKey(e => e.UserId)
                      .HasConstraintName("FK_MenstrualCycle_User");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");
                entity.HasKey(e => e.QuestionId);
                entity.Property(e => e.QuestionId).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                      .WithMany(u => u.QuestionsAsked)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.NoAction)
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

