using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LedEcoKatalog.Data.Scaffold.Entities;

namespace LedEcoKatalog.Data.Scaffold.Context
{
    public partial class KatalogDbContext : DbContext
    {
        public KatalogDbContext()
        {
        }

        public KatalogDbContext(DbContextOptions<KatalogDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hierarchy> Hierarchy { get; set; }
        public virtual DbSet<HierarchyFlatten> HierarchyFlatten { get; set; }
        public virtual DbSet<HierarchyHelp> HierarchyHelp { get; set; }
        public virtual DbSet<HierarchyOnlyLeafs> HierarchyOnlyLeafs { get; set; }
        public virtual DbSet<IkonaRelatedPictures> IkonaRelatedPictures { get; set; }
        public virtual DbSet<PriceLevel> PriceLevel { get; set; }
        public virtual DbSet<ProductBase> ProductBase { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<RelatedPictures> RelatedPictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=LEDeco_Katalog;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hierarchy>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Hierarchy");

                entity.Property(e => e.Path).IsUnicode(false);
            });

            modelBuilder.Entity<HierarchyFlatten>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HierarchyFlatten");
            });

            modelBuilder.Entity<HierarchyHelp>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HierarchyHelp");

                entity.Property(e => e.Popis).IsUnicode(false);
            });

            modelBuilder.Entity<HierarchyOnlyLeafs>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("HierarchyOnlyLeafs");

                entity.Property(e => e.Popis).IsUnicode(false);
            });

            modelBuilder.Entity<IkonaRelatedPictures>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IkonaRelatedPictures");
            });

            modelBuilder.Entity<PriceLevel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PriceLevel");
            });

            modelBuilder.Entity<ProductBase>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProductBase");

                entity.Property(e => e.IntIdproductBase).ValueGeneratedOnAdd();

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Properties>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Properties");
            });

            modelBuilder.Entity<RelatedPictures>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("RelatedPictures");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
