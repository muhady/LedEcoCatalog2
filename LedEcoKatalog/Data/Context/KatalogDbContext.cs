﻿//----------------------------------------------------------------------------
// <copyright file="KatalogDbContext.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using Microsoft.EntityFrameworkCore;

  public partial class KatalogDbContext : DbContext
  {
    #region Public Constructors

    public KatalogDbContext()
    {
    }

    public KatalogDbContext(DbContextOptions<KatalogDbContext> options)
        : base(options)
    {
    }

    #endregion

    #region Public Properties

    public virtual DbSet<Hierarchy> Hierarchy { get; set; }

    public virtual DbSet<HierarchyFlatten> HierarchyFlatten { get; set; }

    public virtual DbSet<HierarchyHelp> HierarchyHelp { get; set; }

    public virtual DbSet<HierarchyOnlyLeafs> HierarchyOnlyLeafs { get; set; }

    public virtual DbSet<IkonaRelatedPictures> IkonaRelatedPictures { get; set; }

    public virtual DbSet<PriceLevel> PriceLevel { get; set; }

    public virtual DbSet<ProductBase> ProductBase { get; set; }

    public virtual DbSet<Properties> Properties { get; set; }

    public virtual DbSet<RelatedPictures> RelatedPictures { get; set; }

    #endregion

    #region Public Properties (Stored Procedures)

    public virtual DbSet<CatalogContentt> CatalogContentt { get; set; }

    public virtual DbSet<CatalogSection1t> CatalogSection1t { get; set; }

    public virtual DbSet<CatalogSection2t> CatalogSection2t { get; set; }

    public virtual DbSet<CatalogSection2pict> CatalogSection2pict { get; set; }

    public virtual DbSet<CatalogSection2pic3rdt> CatalogSection2pic3rdt { get; set; }

    public virtual DbSet<CatalogSection3t> CatalogSection3t { get; set; }

    public virtual DbSet<CatalogSection4t> CatalogSection4t { get; set; }

    #endregion

    #region Protected Methods

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer("name=KatalogDbConnectionString");
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

      ConfigureResults(modelBuilder);

      OnModelCreatingPartial(modelBuilder);
    }

    #endregion

    #region Private Methods

    private void ConfigureResults(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CatalogContentt>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.PathToCategory).IsUnicode(false);
        entity.Property(e => e.Ordr).HasColumnType("numeric(38, 0)");
      });

      modelBuilder.Entity<CatalogSection1t>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.PathToCategory).IsUnicode(false);
        entity.Property(e => e.KratkyPopisKategorie).HasColumnType("ntext");
        entity.Property(e => e.BannerImage).HasColumnType("image");
        entity.Property(e => e.PictureImage).HasColumnType("image");
        entity.Property(e => e.PopisProduktuParent).HasColumnType("ntext");
      });

      modelBuilder.Entity<CatalogSection2t>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.PopisDlhý1).HasColumnType("ntext");
        entity.Property(e => e.PopisDlhý2).HasColumnType("ntext");
      });

      modelBuilder.Entity<CatalogSection2pict>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.Img).HasColumnType("image");
        entity.Property(e => e.BlobData2).HasColumnType("image");
      });

      modelBuilder.Entity<CatalogSection2pic3rdt>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.Img).HasColumnType("image");
        entity.Property(e => e.BlobData2).HasColumnType("image");
      });

      modelBuilder.Entity<CatalogSection3t>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.Img).HasColumnType("image");
      });

      modelBuilder.Entity<CatalogSection4t>(entity =>
      {
        entity.HasNoKey();
      });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    #endregion
  }
}
