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

    public virtual DbSet<Rule> Rules { get; set; }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblBuilding> TblBuildings { get; set; }

    public virtual DbSet<TblFloor> TblFloors { get; set; }


    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblRoom> TblRooms { get; set; }

    public virtual DbSet<TblService> TblServices { get; set; }

    public virtual DbSet<TblServiceUsage> TblServiceUsages { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }


    public virtual DbSet<Violation> Violations { get; set; }

    //    protected override void onconfiguring(dbcontextoptionsbuilder optionsbuilder)
    //#warning to protect potentially sensitive information in your connection string, you should move it out of source code. you can avoid scaffolding the connection string by using the name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. for more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?linkid=723263.
    //        => optionsbuilder.usesqlserver("data source=desktop-l7hk7re;initial catalog=cnpm5;integrated security=true;trustservercertificate=true;");
    //        => optionsBuilder.UseSqlServer("Data Source= LAPTOP-SFNTDCJC\\ANHTAI;Initial Catalog=CNPM5;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rule>(entity =>
        {
            entity.HasKey(e => e.RuleId).HasName("PK__Rules__110458E2AFD5FF3D");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EffectiveDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Penalty).HasMaxLength(100);
            entity.Property(e => e.RuleName).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
           
        });

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


        modelBuilder.Entity<TblService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__tblServi__BD1A23BC336A9BEA");

            entity.ToTable("tblServices");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.Unit).HasMaxLength(20);
        });

        modelBuilder.Entity<TblServiceUsage>(entity =>
        {
            entity.HasKey(e => e.ServiceUsageId).HasName("PK__tblServi__650316FD6D6B662D");

            entity.ToTable("tblServiceUsage");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsBilled).HasDefaultValue(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.TotalCost).HasColumnType("decimal(14, 2)");

            entity.HasOne(d => d.Room).WithMany(p => p.TblServiceUsages)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblServic__RoomI__01142BA1");

            entity.HasOne(d => d.Service).WithMany(p => p.TblServiceUsages)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblServic__Servi__02084FDA");
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__tblStude__32C52B99E474E863");

            entity.ToTable("tblStudent");

            entity.HasIndex(e => e.StudentCode, "UQ__tblStude__1FC886044F91963A").IsUnique();

            entity.HasIndex(e => e.CitizenId, "UQ__tblStude__6E49FBED8498BE33").IsUnique();

            entity.Property(e => e.AvatarUrl).HasMaxLength(250);
            entity.Property(e => e.CitizenId)
                .HasMaxLength(20)
                .HasColumnName("CitizenID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmergencyContactName).HasMaxLength(100);
            entity.Property(e => e.EmergencyContactPhone).HasMaxLength(20);
            entity.Property(e => e.Faculty).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Major).HasMaxLength(100);
            entity.Property(e => e.PermanentAddress).HasMaxLength(250);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.StudentCode).HasMaxLength(20);
            entity.Property(e => e.StudentStatus).HasMaxLength(100);
            entity.Property(e => e.TemporaryAddress).HasMaxLength(250);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.TblStudents)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__tblStuden__Accou__6754599E");
        });


        modelBuilder.Entity<Violation>(entity =>
        {
            entity.HasKey(e => e.ViolationId).HasName("PK__Violatio__18B6DC086E212BF3");
            entity.Property(e => e.ViolationDate)
            .HasColumnType("datetime")
            .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.Note).HasMaxLength(255);

            entity.HasOne(d => d.Rule).WithMany(p => p.Violations)
                .HasForeignKey(d => d.RuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Violation__RuleI__3B40CD36");

            entity.HasOne(d => d.Student).WithMany(p => p.Violations)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Violation__Stude__3A4CA8FD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
