using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LoginAndVegitable.Models;

public partial class VegetableListContext : DbContext
{
    public VegetableListContext()
    {
    }

    public VegetableListContext(DbContextOptions<VegetableListContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Price> Prices { get; set; }

    public virtual DbSet<VegList> VegLists { get; set; }

    public virtual DbSet<VegNamesForlist> VegNamesForlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MSI\\SQLEXPRESS;Database=vegetable_list;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Prices__3214EC07E091DDBE");

            entity.Property(e => e.Price1).HasColumnName("Price");

            entity.HasOne(d => d.Vegetable).WithMany(p => p.Prices)
                .HasForeignKey(d => d.VegetableId)
                .HasConstraintName("FK__Prices__Vegetabl__72C60C4A");

            entity.HasOne(d => d.VegetableNavigation).WithMany(p => p.Prices)
                .HasForeignKey(d => d.VegetableId)
                .HasConstraintName("FK__Prices__Vegetabl__71D1E811");
        });

        modelBuilder.Entity<VegList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__veg_list__3214EC0714B2F0B9");

            entity.ToTable("veg_list");

            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        modelBuilder.Entity<VegNamesForlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__veg_name__3214EC0771980208");

            entity.ToTable("veg_namesForlist");

            entity.Property(e => e.VegetableName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
