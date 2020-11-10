//----------------------------------------------------------------------------
// <copyright file="CatalogModel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;

  using LedEcoKatalog.Data;

  using Microsoft.EntityFrameworkCore;

  public class CatalogModel : AppDataModel
  {
    #region Public Properties

    public int Scope { get; set; }

    public int PriceLevel { get; set; }

    public string Language { get; set; }

    public string Layout { get; set; }

    public string NameOfCatalog { get; set; }

    #endregion

    #region Public Properties

    public int InitialPages { get; set; }

    public string PriceLevelText { get; set; }

    public string CatalogName { get; set; }

    public List<ContentItem> ContentItems { get; set; }

    public List<LegendItem> LegendItems { get; set; }

    public string LegendContent { get; set; }

    public List<PageInfo> PageInfos { get; set; }

    public List<Product> Products { get; set; }

    public List<ProductPicture> ProductPictures { get; set; }

    public List<CatalogSection2pic3rdt> Section2Pic3Rdts { get; set; }

    public List<Accessory> Accessories { get; set; }

    public string[] FosaliLevels { get; set; }

    public int EndPages { get; set; }

    public string DisclaimerContent { get; set; }

    #endregion

    #region Public Properties

    public List<PageModel> Pages { get; set; }

    public CatalogLanguage CatalogLanguage { get; set; }

    #endregion

    #region Public Methods

    public async override Task ExecuteAsync()
    {
      var scope = Scope;
      var priceLevel = PriceLevel;
      var language = Language;
      var visual = Layout;
      var nameOfCatalog = NameOfCatalog;

      if (Settings.CatalogSettings.TryGetValue(visual, out CatalogSettings catalogSettings))
      {
        InitialPages = catalogSettings.InitialPages;
        EndPages = catalogSettings.FinalPages;
      }

      LegendContent = ResourceHelper.GetLegend(visual, language);

      DisclaimerContent = ResourceHelper.GetDisclaimer(visual, language);

      FosaliLevels = Settings.FosaliLayouts.Select(i => i.ToString()).ToArray();

      if (priceLevel == -1)
      {
        PriceLevelText = "Bez cien";
      }
      else
      {
        PriceLevelText = DataContext.PriceLevel.First(e => e.IntIdpriceList == priceLevel).StrTextId.ToString();
      }

      ContentItems = await DataContext.ContentItems
          .FromSqlRaw($"catalogcontent {scope}, {language}")
          .ToListAsync();

      if (nameOfCatalog == null)
      {
        CatalogName = ContentItems.OrderBy(e => e.Page).First().CategoryName;
      }
      else
      {
        CatalogName = nameOfCatalog;
      }

      PageInfos = await DataContext.PageInfos
          .FromSqlRaw($"catalogsection1 {scope}, {language}")
          .ToListAsync();

      Products = await DataContext.Products
        .FromSqlRaw($"catalogsection2 {scope}, {language}, {priceLevel}")
        .ToListAsync();

      ProductPictures = await DataContext.ProductPictures
        .FromSqlRaw($"catalogsection2pic {scope}, {language}")
        .ToListAsync();

      Section2Pic3Rdts = await DataContext.CatalogSection2pic3rdt
        .FromSqlRaw($"catalogsection2pic3rd {scope}, {language}")
        .ToListAsync();

      Accessories = await DataContext.Accessories
        .FromSqlRaw($"catalogsection3 {scope}, {language}, {priceLevel}")
        .ToListAsync();

      var fosaliENproperties = 0;
      if (visual == "Fosali")
      {
        fosaliENproperties = 1;
      }
      else
      {
        fosaliENproperties = 0;
      }

      LegendItems = await DataContext.LegendItem
        .FromSqlRaw($"catalogsection4 {scope}, {language},{fosaliENproperties} ")
        .ToListAsync();

      if (visual == "Fosali")
      {
        language = Settings.DefaultCatalogLanguage;
      }

      CatalogLanguage = Settings.GetCatalogLanguage(language);

      Pages = PageInfos.Select(page => new PageModel
      {
        PageInfo = page,
        Products = Products.Where(e => e.Page == page.Number).OrderBy(e => e.Poradie).ToList(),
        ProductPictures = ProductPictures.Where(e => e.Page == page.Number).ToList(),
        Section2Pic3Rdts = Section2Pic3Rdts.Where(e => e.Page == page.Number).ToList(),
        Accessories = Accessories.Where(e => e.Page == page.Number).ToList(),
        LegendItems = LegendItems.Where(e => e.Page == page.Number).ToList(),

        Language = Language,
        PriceLevel = PriceLevel,
        CatalogLanguage = CatalogLanguage,
        Style = FosaliLevels.Contains(page.Level.ToString()) ? "Fosali" : "Ledeco",
      })
        .ToList();
    }

    /*
    public override async Task OnCreatingViewResultAsync()
    {
    }
    */

    #endregion

    #region Private Methods

    /*
    private List<PageModel> Pages()
    {
      PageModel l = new PageModel();
      l.PageInfo = page;
      l.Products = Model.Products.Where(e => e.Page == page.Number).OrderBy(e => e.Poradie).ToList();
      l.ProductPictures = Model.ProductPictures.Where(e => e.Page == page.Number).ToList();
      l.Section2Pic3Rdts = Model.Section2Pic3Rdts.Where(e => e.Page == page.Number).ToList();
      l.Accessories = Model.Accessories.Where(e => e.Page == page.Number).ToList();
      l.LegendItems = Model.LegendItems.Where(e => e.Page == page.Number).ToList();

      l.Language = Model.Language;
      l.PriceLevel = Model.PriceLevel;
      l.CatalogLanguage = Model.CatalogLanguage;
    }
    */

    #endregion
  }
}
