//----------------------------------------------------------------------------
// <copyright file="KatalogDbContext.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System.Linq;

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

    #region Public Properties (Views)

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

    public virtual DbSet<ContentItem> ContentItems { get; set; }

    public virtual DbSet<LegendItem> LegendItems { get; set; }

    public virtual DbSet<Page> Pages { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductPicture> ProductPictures { get; set; }

    public virtual DbSet<ProductPicture> ProductPictures2 { get; set; }

    public virtual DbSet<Accessory> Accessories { get; set; }

    #endregion

    #region Public Methods (Stored Procedures)

    public IQueryable<ContentItem> GetContentItems(int scope, string language)
    {
      return ContentItems.FromSqlRaw($"catalogcontent {scope}, {language}");
    }

    public IQueryable<LegendItem> GetLegendItems(int scope, string language, bool isFosali)
    {
      return LegendItems.FromSqlRaw($"catalogsection4 {scope}, {language}, {(isFosali ? 1 : 0)}");
    }

    public IQueryable<Page> GetPages(int scope, string language)
    {
      return Pages.FromSqlRaw($"catalogsection1 {scope}, {language}");
    }

    public IQueryable<Product> GetProducts(int scope, string language, int priceLevel)
    {
      return Products.FromSqlRaw($"catalogsection2 {scope}, {language}, {priceLevel}");
    }

    public IQueryable<ProductPicture> GetProductPictures(int scope, string language)
    {
      return ProductPictures.FromSqlRaw($"catalogsection2pic {scope}, {language}");
    }

    public IQueryable<ProductPicture> GetProductPictures2(int scope, string language)
    {
      return ProductPictures2.FromSqlRaw($"catalogsection2pic3rd {scope}, {language}");
    }

    public IQueryable<Accessory> GetAccessories(int scope, string language, int priceLevel)
    {
      return Accessories.FromSqlRaw($"catalogsection3 {scope}, {language}, {priceLevel}");
    }

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
      modelBuilder.Entity<ContentItem>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.PathToCategory).IsUnicode(false);
        entity.Property(e => e.Ordr).HasColumnType("numeric(38, 0)");
      });

      modelBuilder.Entity<LegendItem>(entity =>
      {
        entity.HasNoKey();
      });

      modelBuilder.Entity<Page>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.PathToCategory).IsUnicode(false);
        entity.Property(e => e.ShortCategoryDescription).HasColumnType("ntext");
        entity.Property(e => e.BannerImage).HasColumnType("image");
        entity.Property(e => e.PictureImage).HasColumnType("image");
        entity.Property(e => e.ParentProductDescription).HasColumnType("ntext");
      });

      modelBuilder.Entity<Product>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.PopisDlhý1).HasColumnType("ntext");
        entity.Property(e => e.PopisDlhý2).HasColumnType("ntext");
      });

      modelBuilder.Entity<ProductPicture>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.Img).HasColumnType("image");
        entity.Property(e => e.BlobData2).HasColumnType("image");
      });

      modelBuilder.Entity<Accessory>(entity =>
      {
        entity.HasNoKey();
        entity.Property(e => e.Image).HasColumnType("image");
      });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    #endregion
  }
}
