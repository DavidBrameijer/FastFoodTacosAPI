﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TacoFastFoodAPI.Models;

public partial class FastFoodTacoDbContext : DbContext
{
    public FastFoodTacoDbContext()
    {
    }

    public FastFoodTacoDbContext(DbContextOptions<FastFoodTacoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Combo> Combos { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<Taco> Tacos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=FastFoodTacoDB; Integrated Security=SSPI;Encrypt=false;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Combo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Combo__3214EC2713616264");

            entity.ToTable("Combo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Drink).WithMany(p => p.Combos)
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("FK__Combo__DrinkId__4E88ABD4");

            entity.HasOne(d => d.Taco).WithMany(p => p.Combos)
                .HasForeignKey(d => d.TacoId)
                .HasConstraintName("FK__Combo__TacoId__4D94879B");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drink__3214EC2792972D27");

            entity.ToTable("Drink");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Taco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Taco__3214EC279D7A8676");

            entity.ToTable("Taco");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}