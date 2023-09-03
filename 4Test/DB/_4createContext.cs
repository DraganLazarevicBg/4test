using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB
{
    public partial class _4createContext : DbContext
    {
        public _4createContext()
        {
        }

        public _4createContext(DbContextOptions<_4createContext> options)
            : base(options)
        {
        }

		

		public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Systemlog> Systemlogs { get; set; } = null!;
        public virtual DbSet<Work> Works { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("user id=root;host=localhost;password=xxx;character set=utf8;database=4create", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.1.0-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .HasColumnName("email");

                entity.Property(e => e.Title)
                    .HasColumnType("enum('developer','manager','tester')")
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Systemlog>(entity =>
            {
                entity.ToTable("systemlog");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Attributes).HasColumnName("attributes");

                entity.Property(e => e.Comment)
                    .HasMaxLength(4000)
                    .HasColumnName("comment");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.Event)
                    .HasMaxLength(255)
                    .HasColumnName("event");

                entity.Property(e => e.ResourceIdentifier).HasColumnName("resourceIdentifier");

                entity.Property(e => e.ResourceType)
                    .HasMaxLength(200)
                    .HasColumnName("resourceType");
            });

            modelBuilder.Entity<Work>(entity =>
            {
                entity.ToTable("work");

                entity.HasIndex(e => e.Comany, "FK_work_company");

                entity.HasIndex(e => e.Employee, "FK_work_employee");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comany).HasColumnName("comany");

                entity.Property(e => e.Employee).HasColumnName("employee");

                entity.HasOne(d => d.ComanyNavigation)
                    .WithMany(p => p.Works)
                    .HasForeignKey(d => d.Comany)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_work_company");

                entity.HasOne(d => d.EmployeeNavigation)
                    .WithMany(p => p.Works)
                    .HasForeignKey(d => d.Employee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_work_employee");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
