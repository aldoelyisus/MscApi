using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using MscApi.Domain.Entities;
namespace MscApi.DataAccess;

public partial class MedStaCruzContext : DbContext
{
    public MedStaCruzContext()
    {
    }

    public MedStaCruzContext(DbContextOptions<MedStaCruzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CartItem> CartItems { get; set; } = null!;

    public virtual DbSet<CatalogueType> CatalogueTypes { get; set; } = null!;

    public virtual DbSet<CategoryType> Categorytypes { get; set; } = null!;

    public virtual DbSet<CompanyInformation> CompanyInformation { get; set; } = null!;

    public virtual DbSet<Product> Products { get; set; } = null!;

    public virtual DbSet<Reservation> Reservations { get; set; } = null!;

    public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; } = null!;

    public virtual DbSet<ReservationType> ReservationTypes { get; set; } = null!;

    public virtual DbSet<RoleType> RoleTypes { get; set; } = null!;

    public virtual DbSet<StoreLocation> StoreLocations { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("Name=ConnectionStrings:MscDBServer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cartitems");

            entity.HasIndex(e => e.Reservation, "fk_Reservation");

            entity.HasIndex(e => new { e.Product, e.Reservation }, "uk_ProdR").IsUnique();

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.Product)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Product");

            entity.HasOne(d => d.ReservationNavigation).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.Reservation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Reservation");
        });

        modelBuilder.Entity<CatalogueType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cataloguetype");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(15);
        });

        modelBuilder.Entity<CategoryType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categorytype");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.Label).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<CompanyInformation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("companyinformation");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.LogoPath)
                .HasMaxLength(100)
                .HasDefaultValueSql("'./img/no_image.jpg'");
            entity.Property(e => e.Mission).HasColumnType("text");
            entity.Property(e => e.Vision).HasColumnType("text");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.Catalogue, "fk_CType");

            entity.HasIndex(e => e.Category, "fk_SPType");

            entity.HasIndex(e => new { e.Name, e.Model, e.Brand }, "uk_Details").IsUnique();

            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.Catalogue).HasMaxLength(15);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ImgPath)
                .HasMaxLength(100)
                .HasDefaultValueSql("'./img/no_image.jpg'");
            entity.Property(e => e.Model).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.CatalogueNavigation).WithMany(p => p.Products)
                .HasPrincipalKey(p => p.Name)
                .HasForeignKey(d => d.Catalogue)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CType");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Products)
                .HasPrincipalKey(p => p.Name)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SPType");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reservations");

            entity.HasIndex(e => e.StatusHistory, "StatusHistory").IsUnique();

            entity.HasOne(d => d.StatusHistoryNavigation).WithOne(p => p.Reservation)
                .HasForeignKey<Reservation>(d => d.StatusHistory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_History");
        });

        modelBuilder.Entity<ReservationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reservationstatus");

            entity.HasIndex(e => e.Code, "fk_Status");

            entity.Property(e => e.Code).HasMaxLength(15);
            entity.Property(e => e.StatusDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.CodeNavigation).WithMany(p => p.ReservationStatuses)
                .HasPrincipalKey(p => p.Name)
                .HasForeignKey(d => d.Code)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Status");
        });

        modelBuilder.Entity<ReservationType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reservationtype");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.Label).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(15);
        });

        modelBuilder.Entity<RoleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roletype");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.Label).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<StoreLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("storelocations");

            entity.HasIndex(e => e.Name, "Name").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasDefaultValueSql("''");
            entity.Property(e => e.FacebookUser)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Telephone)
                .HasMaxLength(15)
                .HasDefaultValueSql("''");
            entity.Property(e => e.TwitterUser)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.WhatsAppPhone)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "Email").IsUnique();

            entity.HasIndex(e => e.Role, "fk_Role");

            entity.HasIndex(e => new { e.UserName, e.Email }, "uk_NM").IsUnique();

            entity.HasIndex(e => new { e.UserName, e.Role }, "uk_NR").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstSurname)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.LastSurname)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasDefaultValueSql("''");
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Role).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(15);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasPrincipalKey(p => p.Name)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
