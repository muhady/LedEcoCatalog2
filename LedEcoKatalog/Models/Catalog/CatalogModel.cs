//----------------------------------------------------------------------------
// <copyright file="CatalogModel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using System;
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

    public CatalogLanguage CatalogLanguage { get; set; }

    public int FrontPageCount { get; set; }

    public string PriceLevelText { get; set; }

    public List<ContentItem> ContentItems { get; set; }

    public List<LegendItem> LegendItems { get; set; }

    public string LegendContent { get; set; }

    public List<PageModel> Pages { get; set; }

    public int BackPageCount { get; set; }

    public string Disclaimer { get; set; }

    #endregion

    #region Public Methods

    public async override Task ExecuteAsync()
    {
      var scope = Scope;
      var priceLevel = PriceLevel;
      var language = Language;
      var layout = Layout;

      var isFosali = string.Equals(layout, "Fosali", StringComparison.OrdinalIgnoreCase);

      CatalogLanguage = Settings.GetCatalogLanguage(isFosali ? Settings.FosaliLanguageCode : language);

      if (Settings.CatalogSettings.TryGetValue(layout, out CatalogSettings catalogSettings))
      {
        FrontPageCount = catalogSettings.FrontPageCount;
        BackPageCount = catalogSettings.BackPageCount;
      }

      ContentItems = await DataContext.GetContentItems(scope, language).ToListAsync();

      if (string.IsNullOrEmpty(Name))
      {
        Name = ContentItems.OrderBy(e => e.Page).FirstOrDefault()?.CategoryName;
      }

      PriceLevelText = priceLevel == -1 ? CatalogLanguage?.NoPrices : DataContext.PriceLevel.FirstOrDefault(e => e.IntIdpriceList == priceLevel)?.StrTextId?.ToString();

      LegendItems = await DataContext.GetLegendItems(scope, language, isFosali).ToListAsync();
      LegendContent = ResourceHelper.GetLegend(layout, language);

      var pages = await DataContext.GetPageInfos(scope, language).ToListAsync();
      var products = await DataContext.GetProducts(scope, language, priceLevel).ToListAsync();
      var productPictures = await DataContext.GetProductPictures(scope, language).ToListAsync();
      var section2Pic3Rdts = await DataContext.GetProductPictures2(scope, language).ToListAsync();
      var accessories = await DataContext.GetAccessories(scope, language, priceLevel).ToListAsync();

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
        Style = Settings.FosaliLayouts.Contains(page.Level) ? "Fosali" : "Ledeco",
      })
        .ToList();

      Disclaimer = ResourceHelper.GetDisclaimer(layout, language);
    }

    #endregion
  }
}
