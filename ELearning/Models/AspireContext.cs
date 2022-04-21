using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ELearning.Models
{
    public partial class AspireContext : DbContext
    {
        public AspireContext()
        {
        }

        public AspireContext(DbContextOptions<AspireContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ELearningDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.ToTable("Grade");

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.GradeNameAr).HasMaxLength(150);

                entity.Property(e => e.GradeNameEn).HasMaxLength(150);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Birthdate).HasColumnType("date");

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.StudentNameAr).HasMaxLength(150);

                entity.Property(e => e.StudentNameEn).HasMaxLength(150);

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.GradeId)
                    .HasConstraintName("FK_Student_Grade");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
