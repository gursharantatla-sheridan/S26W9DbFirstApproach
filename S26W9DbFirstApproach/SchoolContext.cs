using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace S26W9DbFirstApproach;

// context class
public partial class SchoolContext : DbContext
{
    public SchoolContext()
    {
    }

    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    // entity sets
    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Standard> Standards { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAddress> StudentAddresses { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connStr = ConfigurationManager.ConnectionStrings["School"].ConnectionString;
        optionsBuilder.UseSqlServer(connStr);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Course__C92D71A73A0AC888");

            entity.ToTable("Course");

            entity.Property(e => e.CourseName).HasMaxLength(50);

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Course_Teacher");
        });

        modelBuilder.Entity<Standard>(entity =>
        {
            entity.HasKey(e => e.StandardId).HasName("PK__Standard__BB33D20CAD678B6A");

            entity.ToTable("Standard");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.StandardName).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student__32C52B9960627E6C");

            entity.ToTable("Student");

            entity.Property(e => e.StudentName).HasMaxLength(50);

            entity.HasOne(d => d.Standard).WithMany(p => p.Students)
                .HasForeignKey(d => d.StandardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Student_Standard");

            entity.HasMany(d => d.Courses).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "StudentCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_StudentCourse_Course"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_StudentCourse_Student"),
                    j =>
                    {
                        j.HasKey("StudentId", "CourseId").HasName("PK__StudentC__5E57FC83D413CC65");
                        j.ToTable("StudentCourse");
                    });
        });

        modelBuilder.Entity<StudentAddress>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__StudentA__32C52B99D5C21A9A");

            entity.ToTable("StudentAddress");

            entity.Property(e => e.StudentId).ValueGeneratedNever();
            entity.Property(e => e.Address1).HasMaxLength(50);
            entity.Property(e => e.Address2).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Province).HasMaxLength(50);

            entity.HasOne(d => d.Student).WithOne(p => p.StudentAddress)
                .HasForeignKey<StudentAddress>(d => d.StudentId)
                .HasConstraintName("FK_StudentAddress_Student");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__Teacher__EDF259640730C0C3");

            entity.ToTable("Teacher");

            entity.Property(e => e.TeacherName).HasMaxLength(50);

            entity.HasOne(d => d.Standard).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.StandardId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Teacher_Standard");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
