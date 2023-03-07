using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project7MAR2023.PGModels;

public partial class PostgresContext : DbContext
{
    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Divisi> Divisis { get; set; }

    public virtual DbSet<Kategori> Kategoris { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=12345;Database=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pg_catalog", "adminpack");

        modelBuilder.Entity<Divisi>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("divisi");

            entity.Property(e => e.Ids).HasColumnName("ids");
            entity.Property(e => e.Nama)
                .HasMaxLength(200)
                .HasColumnName("nama");
        });

        modelBuilder.Entity<Kategori>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("KATEGORI");

            entity.Property(e => e.Ids).HasColumnName("IDS");
            entity.Property(e => e.Nama)
                .HasMaxLength(300)
                .HasColumnName("NAMA");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Ids).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Ids).HasColumnName("ids");
            entity.Property(e => e.Keterangan)
                .HasMaxLength(100)
                .HasColumnName("keterangan");
            entity.Property(e => e.Nama)
                .HasMaxLength(100)
                .HasColumnName("nama");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
