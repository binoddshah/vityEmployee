using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace vityEmployeeInfoSystem.Models;

public partial class AgroEmployeeDbContext : DbContext
{
    public AgroEmployeeDbContext()
    {
    }

    public AgroEmployeeDbContext(DbContextOptions<AgroEmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepartmentTable> DepartmentTables { get; set; }

    public virtual DbSet<DesignationTable> DesignationTables { get; set; }

    public virtual DbSet<EmployeeTable> EmployeeTables { get; set; }

    public virtual DbSet<LeaveTable> LeaveTables { get; set; }

    public virtual DbSet<SalaryTable> SalaryTables { get; set; }

    public virtual DbSet<UserTable> UserTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=AgroEmployeeDB;TrustServerCertificate=True;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepartmentTable>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED0DAF0CB0");

            entity.ToTable("DepartmentTable");

            entity.HasIndex(e => e.DepartmentName, "UQ__Departme__D949CC34108B795B").IsUnique();

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description).IsUnicode(false);
        });

        modelBuilder.Entity<DesignationTable>(entity =>
        {
            entity.HasKey(e => e.DesignationId).HasName("PK__Designat__BABD60DE060DEAE8");

            entity.ToTable("DesignationTable");

            entity.HasIndex(e => e.DesignationName, "UQ__Designat__372CDC2308EA5793").IsUnique();

            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.DesignationName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EmployeeTable>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04F1115502E78");

            entity.ToTable("EmployeeTable");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Contact)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Department).WithMany(p => p.EmployeeTables)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__EmployeeT__Depar__182C9B23");

            entity.HasOne(d => d.Designation).WithMany(p => p.EmployeeTables)
                .HasForeignKey(d => d.DesignationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__EmployeeT__Desig__173876EA");
        });

        modelBuilder.Entity<LeaveTable>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__LeaveTab__796DB9591BFD2C07");

            entity.ToTable("LeaveTable");

            entity.Property(e => e.Reason)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveTables)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__LeaveTabl__Emplo__1DE57479");
        });

        modelBuilder.Entity<SalaryTable>(entity =>
        {
            entity.HasKey(e => e.SalaryId).HasName("PK__salaryTa__33631F9822AA2996");

            entity.ToTable("salaryTable");

            entity.Property(e => e.SalaryId).HasColumnName("salaryId");
            entity.Property(e => e.EmployeeId).HasColumnName("employeeId");
            entity.Property(e => e.ReleasedAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReleasedForMonth)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.SalaryTables)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__salaryTab__emplo__24927208");
        });

        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__userTabl__1788CC4C7F60ED59");

            entity.ToTable("userTable");

            entity.HasIndex(e => e.UserName, "UQ__userTabl__C9F28456023D5A04").IsUnique();

            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
