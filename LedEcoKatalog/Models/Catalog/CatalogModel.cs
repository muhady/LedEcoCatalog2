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

    public int ScopeId { get; set; }

    public int PriceLevelId { get; set; }

    public string LanguageCode { get; set; }

    public string LayoutCode { get; set; }

    public string Name { get; set; }

    #endregion

    #region Public Properties

    public CatalogLayout Layout { get; set; }

    public CatalogLanguage Language { get; set; }

    public string PriceLevelText { get; set; }

    public List<ContentItem> ContentItems { get; set; }

    public List<LegendItem> LegendItems { get; set; }

    public string LegendContent { get; set; }

    public List<PageModel> Pages { get; set; }

    public string Disclaimer { get; set; }

    #endregion

    #region Public Methods

    public async override Task ExecuteAsync()
    {
      var scope = ScopeId;
      var priceLevel = PriceLevelId;
      var language = LanguageCode;
      var layout = LayoutCode;

      var isFosali = string.Equals(layout, "Fosali", StringComparison.OrdinalIgnoreCase);

      Layout = Settings.CatalogLayouts.TryGetValue(layout, out CatalogLayout catalogLayout) ? catalogLayout : new CatalogLayout();
      Language = Settings.GetCatalogLanguage(isFosali ? Settings.FosaliLanguageCode : language) ?? new CatalogLanguage();

      ContentItems = await DataContext.GetContentItems(scope, language).ToListAsync();

      if (string.IsNullOrEmpty(Name))
      {
        Name = ContentItems.OrderBy(e => e.Page).FirstOrDefault()?.CategoryName;
      }

      PriceLevelText = priceLevel == -1 ? Language?.NoPrices : DataContext.PriceLevel.FirstOrDefault(e => e.IntIdpriceList == priceLevel)?.StrTextId?.ToString();

      LegendItems = await DataContext.GetLegendItems(scope, language, isFosali).ToListAsync();
      LegendContent = ResourceHelper.GetLegend(layout, language);

      var pages = await DataContext.GetPages(scope, language).ToListAsync();
      var products = await DataContext.GetProducts(scope, language, priceLevel).ToListAsync();
      var productPictures = await DataContext.GetProductPictures(scope, language).ToListAsync();
      var productPictures2 = await DataContext.GetProductPictures2(scope, language).ToListAsync();
      var accessories = await DataContext.GetAccessories(scope, language, priceLevel).ToListAsync();

      Pages = pages.Select(page => new PageModel
      {
        Language = Language,

        Page = page,
        Products = products.Where(e => e.Page == page.Number).OrderBy(e => e.Poradie).ToList(),
        ProductPictures = productPictures.Where(e => e.Page == page.Number).ToList(),
        ProductPictures2 = productPictures2.Where(e => e.Page == page.Number).ToList(),
        Accessories = accessories.Where(e => e.Page == page.Number).ToList(),
        LegendItems = LegendItems.Where(e => e.Page == page.Number).ToList(),

        IsFosali = Settings.FosaliLayouts.Contains(page.Level),
        HasPrices = PriceLevelId != -1,
      })
        .ToList();

      Disclaimer = ResourceHelper.GetDisclaimer(layout, language);
    }

    #endregion
  }
}
