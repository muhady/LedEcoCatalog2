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

    public string Name { get; set; }

    #endregion

    #region Public Properties

    public int FrontPageCount { get; set; }

    public string PriceLevelText { get; set; }

    public List<ContentItem> ContentItems { get; set; }

    public List<LegendItem> LegendItems { get; set; }

    public string LegendContent { get; set; }

    public List<PageModel> Pages { get; set; }

    public int BackPageCount { get; set; }

    public string Disclaimer { get; set; }

    public CatalogLanguage CatalogLanguage { get; set; }

    #endregion

    #region Public Methods

    public async override Task ExecuteAsync()
    {
      var scope = Scope;
      var priceLevel = PriceLevel;
      var language = Language;
      var layout = Layout;
      var nameOfCatalog = Name;

      if (Settings.CatalogSettings.TryGetValue(layout, out CatalogSettings catalogSettings))
      {
        FrontPageCount = catalogSettings.FrontPageCount;
        BackPageCount = catalogSettings.BackPageCount;
      }

      if (priceLevel == -1)
      {
        PriceLevelText = "Bez cien";
      }
      else
      {
        PriceLevelText = DataContext.PriceLevel.FirstOrDefault(e => e.IntIdpriceList == priceLevel)?.StrTextId?.ToString();
      }

      ContentItems = await DataContext.ContentItems
          .FromSqlRaw($"catalogcontent {scope}, {language}")
          .ToListAsync();

      if (string.IsNullOrEmpty(Name))
      {
        Name = ContentItems.OrderBy(e => e.Page).FirstOrDefault()?.CategoryName;
      }

      var fosaliENproperties = 0;
      if (layout == "Fosali")
      {
        fosaliENproperties = 1;
      }
      else
      {
        fosaliENproperties = 0;
      }

      LegendItems = await DataContext.LegendItem
        .FromSqlRaw($"catalogsection4 {scope}, {language}, {fosaliENproperties}")
        .ToListAsync();

      LegendContent = ResourceHelper.GetLegend(layout, language);

      var pages = await DataContext.PageInfos
          .FromSqlRaw($"catalogsection1 {scope}, {language}")
          .ToListAsync();

      var products = await DataContext.Products
        .FromSqlRaw($"catalogsection2 {scope}, {language}, {priceLevel}")
        .ToListAsync();

      var productPictures = await DataContext.ProductPictures
        .FromSqlRaw($"catalogsection2pic {scope}, {language}")
        .ToListAsync();

      var section2Pic3Rdts = await DataContext.CatalogSection2pic3rdt
        .FromSqlRaw($"catalogsection2pic3rd {scope}, {language}")
        .ToListAsync();

      var accessories = await DataContext.Accessories
        .FromSqlRaw($"catalogsection3 {scope}, {language}, {priceLevel}")
        .ToListAsync();

      if (layout == "Fosali")
      {
        language = Settings.DefaultCatalogLanguage;
      }

      var fosaliLevels = Settings.FosaliLayouts.Select(i => i.ToString()).ToArray();

      CatalogLanguage = Settings.GetCatalogLanguage(language);

      Pages = pages.Select(page => new PageModel
      {
        PageInfo = page,
        Products = products.Where(e => e.Page == page.Number).OrderBy(e => e.Poradie).ToList(),
        ProductPictures = productPictures.Where(e => e.Page == page.Number).ToList(),
        Section2Pic3Rdts = section2Pic3Rdts.Where(e => e.Page == page.Number).ToList(),
        Accessories = accessories.Where(e => e.Page == page.Number).ToList(),
        LegendItems = LegendItems.Where(e => e.Page == page.Number).ToList(),

        Language = Language,
        PriceLevel = PriceLevel,
        CatalogLanguage = CatalogLanguage,
        Style = fosaliLevels.Contains(page.Level.ToString()) ? "Fosali" : "Ledeco",
      })
        .ToList();

      Disclaimer = ResourceHelper.GetDisclaimer(layout, language);
    }

    #endregion
  }
}
