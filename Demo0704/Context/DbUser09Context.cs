using System;
using System.Collections.Generic;
using Demo0704.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo0704.Context;

public partial class DbUser09Context : DbContext
{
    public DbUser09Context()
    {
    }

    public DbUser09Context(DbContextOptions<DbUser09Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Concelarium> Concelaria { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Naming> Namings { get; set; }

    public virtual DbSet<Postavshiki> Postavshikis { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.200.10;Database=db_user09;Username=user09;password=plAydUmBk8XR8hFj");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Concelarium>(entity =>
        {
            entity.HasKey(e => e.Article).HasName("concelaria_pkey");

            entity.ToTable("concelaria");

            entity.Property(e => e.Article)
                .HasMaxLength(255)
                .HasColumnName("article");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CurrentDiscount).HasColumnName("current_discount");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.MaxDiscount).HasColumnName("max_discount");
            entity.Property(e => e.NamingId).HasColumnName("naming_id");
            entity.Property(e => e.PostavshikId).HasColumnName("postavshik_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.QuantityInStorage).HasColumnName("quantity_in_storage");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Concelaria)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("concelaria_category_id_fkey");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Concelaria)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("concelaria_manufacturer_id_fkey");

            entity.HasOne(d => d.Naming).WithMany(p => p.Concelaria)
                .HasForeignKey(d => d.NamingId)
                .HasConstraintName("concelaria_naming_id_fkey");

            entity.HasOne(d => d.Postavshik).WithMany(p => p.Concelaria)
                .HasForeignKey(d => d.PostavshikId)
                .HasConstraintName("concelaria_postavshik_id_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.Concelaria)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("concelaria_unit_id_fkey");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.ManufacturerId).HasName("manufacturer_pkey");

            entity.ToTable("manufacturer");

            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Naming>(entity =>
        {
            entity.HasKey(e => e.NamingId).HasName("naming_pkey");

            entity.ToTable("naming");

            entity.Property(e => e.NamingId).HasColumnName("naming_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Postavshiki>(entity =>
        {
            entity.HasKey(e => e.PostavshikId).HasName("postavshiki_pkey");

            entity.ToTable("postavshiki");

            entity.Property(e => e.PostavshikId).HasColumnName("postavshik_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("units_pkey");

            entity.ToTable("units");

            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.SecondName)
                .HasMaxLength(255)
                .HasColumnName("second_name");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("users_role_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
