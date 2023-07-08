using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BindalBatteryAPI.dbcontext
{
    public partial class bindalbatteryContext : DbContext
    {
        public bindalbatteryContext(DbContextOptions<bindalbatteryContext> options) 
            : base(options)
        {
        }

        public virtual DbSet<Applicationuser> Applicationusers { get; set; } = null!;
        public virtual DbSet<Partymaster> Partymasters { get; set; } = null!;
        public virtual DbSet<Productmaster> Productmasters { get; set; } = null!;
        public virtual DbSet<Replace> Replaces { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<Warantymaster> Warantymasters { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseMySQL("Server=localhost;User ID=root;Password=Satvik@12;Database=bindalbattery");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicationuser>(entity =>
            {
                entity.ToTable("applicationuser");

                entity.HasIndex(e => e.ApplicationUserId, "Pk_ApplicationUserId");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'now()'");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'(''System'')'");

                entity.Property(e => e.IsActive).HasMaxLength(45);

                entity.Property(e => e.PartyName).HasMaxLength(200);

                entity.Property(e => e.Passcode).HasMaxLength(10);

                entity.Property(e => e.Place).HasMaxLength(200);
            });

            modelBuilder.Entity<Partymaster>(entity =>
            {
                entity.ToTable("partymaster");

               

                entity.HasIndex(e => e.PartyMasterId, "Pk_PartyMasterId");

               
                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'now()'");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'(''System'')'");                
            });

            modelBuilder.Entity<Productmaster>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PRIMARY");

                entity.ToTable("productmaster");

                entity.HasIndex(e => e.ProductId, "PK_ProductId");

                //entity.Property(e => e.BatterySrNo).HasMaxLength(45);

                entity.Property(e => e.BatteryType).HasMaxLength(100);

                entity.Property(e => e.BrandName).HasMaxLength(200);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'now()'");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'(''System'')'");
            });

            modelBuilder.Entity<Replace>(entity =>
            {
                entity.HasKey(e => e.ReplaceId).HasName("PRIMARY");

                entity.ToTable("replace");

                entity.HasIndex(e => e.PartyId, "Fk_replace_PartyMaster");

                entity.HasIndex(e => e.ProductId, "Fk_replace_Product");

                entity.HasIndex(e => e.SaleId, "Fk_replace_sale");

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("'now()'")
                    .HasColumnType("datetime");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'(''System'')'");
                entity.Property(e => e.NewSrNo).HasMaxLength(45);
                entity.Property(e => e.ReplacementDate).HasColumnType("datetime");

                entity.HasOne(d => d.Party).WithMany(p => p.Replaces)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("Fk_replace_PartyMaster");

                entity.HasOne(d => d.Product).WithMany(p => p.Replaces)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_replace_Product");

                entity.HasOne(d => d.Sale).WithMany(p => p.Replaces)
                    .HasForeignKey(d => d.SaleId)
                    .HasConstraintName("Fk_replace_sale");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("sales");

                entity.HasIndex(e => e.PartyId, "IX_ApplicationUserid");

                entity.HasIndex(e => e.ProductId, "IX_ProductId");

                entity.HasIndex(e => e.Id, "Pk_SalesId");

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("'now()'")
                    .HasColumnType("datetime");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'(''System'')'");
                entity.Property(e => e.SaleDate).HasColumnType("date");

                entity.HasOne(d => d.Party).WithMany(p => p.Sales)
                    .HasForeignKey(d => d.PartyId)
                    .HasConstraintName("Fk_sales_PartyMaster");

                entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("Fk_sales_Product");
            });


            modelBuilder.Entity<Warantymaster>(entity =>
            {
                entity.HasKey(e => e.WarrantyId).HasName("PRIMARY");

                entity.ToTable("warantymaster");

                entity.HasIndex(e => e.PartyMatserId, "Fk_WarrantyMaster_PartyMaster");

                entity.HasIndex(e => e.ProductId, "Fk_WarrantyMaster_Product");

                entity.Property(e => e.CreateDate)
                    .HasDefaultValueSql("'now()'")
                    .HasColumnType("datetime");
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'(''System'')'");

                entity.HasOne(d => d.PartyMatser).WithMany(p => p.Warantymasters)
                    .HasForeignKey(d => d.PartyMatserId)
                    .HasConstraintName("Fk_WarrantyMaster_PartyMaster");

                entity.HasOne(d => d.ProductMaster).WithMany(p => p.Warantymasters)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("Fk_WarrantyMaster_Product");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
