using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CNPM5.Models;

public partial class Cnpm5Context : DbContext
{
    public Cnpm5Context()
    {
    }

    public Cnpm5Context(DbContextOptions<Cnpm5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblBuilding> TblBuildings { get; set; }

    public virtual DbSet<TblFloor> TblFloors { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblRoom> TblRooms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-L7HK7RE;Initial Catalog=CNPM5;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__tblAccou__349DA5A6AB334ADF");

            entity.ToTable("tblAccount");

            entity.HasIndex(e => e.Username, "UQ__tblAccou__536C85E4CCE54131").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasDefaultValue(2);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<TblBuilding>(entity =>
        {
            entity.HasKey(e => e.BuildingId).HasName("PK__Building__3214EC07C4CD7C40");

            entity.ToTable("tblBuildings");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.TotalFloors).HasDefaultValue(0);
            entity.Property(e => e.TotalRooms).HasDefaultValue(0);
        });

        modelBuilder.Entity<TblFloor>(entity =>
        {
            entity.HasKey(e => e.FloorId).HasName("PK__Floors__3214EC0741DB517C");

            entity.ToTable("tblFloors");

            entity.HasOne(d => d.Building).WithMany(p => p.TblFloors)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Floors__Building__3A81B327");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tblRole__8AFACE1A85DA442B");

            entity.ToTable("tblRole");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Rooms__32863939991F1493");

            entity.ToTable("tblRooms");

            entity.Property(e => e.CurrentOccupants).HasDefaultValue(0);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RoomName).HasMaxLength(50);
            entity.Property(e => e.RoomType).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Trống");

            entity.HasOne(d => d.Floor).WithMany(p => p.TblRooms)
                .HasForeignKey(d => d.FloorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rooms__FloorId__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
