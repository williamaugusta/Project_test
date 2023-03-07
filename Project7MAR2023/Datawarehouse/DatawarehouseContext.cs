using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project7MAR2023.Datawarehouse;

public partial class DatawarehouseContext : DbContext
{
    public DatawarehouseContext()
    {
    }

    public DatawarehouseContext(DbContextOptions<DatawarehouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EmployeeTrainingHistory> EmployeeTrainingHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=datawarehouse;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeTrainingHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EmployeeTraining_pk");

            entity.ToTable("employee_training_history");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Completeddate).HasColumnName("completeddate");
            entity.Property(e => e.Employeeid)
                .HasMaxLength(10)
                .HasColumnName("employeeid");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Postitle)
                .HasMaxLength(500)
                .HasColumnName("postitle");
            entity.Property(e => e.Traningtype)
                .HasMaxLength(500)
                .HasColumnName("traningtype");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
