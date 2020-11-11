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

    public int Year { get; set; }

    public string PriceLevel { get; set; }

    public string FrontCoverContent { get; set; }

    public List<ContentItem> ContentItems { get; set; }

    public List<LegendItem> LegendItems { get; set; }

    public string LegendContent { get; set; }

    public List<PageModel> Pages { get; set; }

    public string Disclaimer { get; set; }

    #endregion

    #region Public Methods

    public async override Task ExecuteAsync()
    {
      var scopeId = ScopeId;
      var priceLevelId = PriceLevelId;
      var languageCode = LanguageCode;
      var layoutCode = LayoutCode;

      var isFosali = string.Equals(layoutCode, "Fosali", StringComparison.OrdinalIgnoreCase);

      Layout = Settings.CatalogLayouts.TryGetValue(layoutCode, out CatalogLayout catalogLayout) ? catalogLayout : new CatalogLayout();
      Language = Settings.GetCatalogLanguage(isFosali ? Settings.FosaliLanguageCode : languageCode) ?? new CatalogLanguage();

      ContentItems = await DataContext.GetContentItems(scopeId, languageCode).ToListAsync();

      Name = string.IsNullOrEmpty(Name) ? ContentItems.OrderBy(e => e.Page).FirstOrDefault()?.CategoryName : Name;
      Year = DateTime.Today.Year + 1;
      PriceLevel = priceLevelId == -1 ? Language?.NoPrices : DataContext.PriceLevel.FirstOrDefault(e => e.IntIdpriceList == priceLevelId)?.StrTextId?.ToString();

      var frontCoverContent = WebContentHelper.GetFrontCoverContent(layoutCode, languageCode);
      if (frontCoverContent != null)
      {
        FrontCoverContent = frontCoverContent.Replace("{{name}}", Name).Replace("{{year}}", Year.ToString()).Replace("{{price-level}}", PriceLevel);
      }

      var legendItems = await DataContext.GetLegendItems(scopeId, languageCode, isFosali).ToListAsync();
      LegendItems = legendItems.Where(e => !e.Value.Contains("::")).GroupBy(e => new { e.StrPropertyNameForCust, e.ImageFileName }).Select(e => e.FirstOrDefault()).ToList();
      LegendContent = AppContentHelper.GetLegend(layoutCode, languageCode);

      var pages = await DataContext.GetPages(scopeId, languageCode).ToListAsync();
      var products = await DataContext.GetProducts(scopeId, languageCode, priceLevelId).ToListAsync();
      var productPictures = await DataContext.GetProductPictures(scopeId, languageCode).ToListAsync();
      var productPictures2 = await DataContext.GetProductPictures2(scopeId, languageCode).ToListAsync();
      var accessories = await DataContext.GetAccessories(scopeId, languageCode, priceLevelId).ToListAsync();

      Pages = pages.Select(page =>
      {
        var pageLegendItems = legendItems.Where(e => e.Page == page.Number).ToList();
        var isFosaliPage = Settings.FosaliLayouts.Contains(page.Level);

        return new PageModel
        {
          Language = Language,

          Page = page,
          LegendItems = pageLegendItems.Where(e => (!e.Value.Contains("::"))).ToList(),
          MultivaluedLegendItems = pageLegendItems.Where(l => l.Value.Contains("::")).ToList(),
          Products = products.Where(e => e.Page == page.Number).OrderBy(e => e.Poradie).ToList(),
          ProductPictures = productPictures.Where(e => e.Page == page.Number).ToList(),
          ProductPictures2 = productPictures2.Where(e => e.Page == page.Number).ToList(),
          Accessories = accessories.Where(e => e.Page == page.Number).ToList(),

          IsFosali = isFosaliPage,
          HasPrices = PriceLevelId != -1,
        };
      }).ToList();

      Disclaimer = AppContentHelper.GetDisclaimer(layoutCode, languageCode);
    }

    #endregion
  }
}
