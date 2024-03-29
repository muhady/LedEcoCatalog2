//----------------------------------------------------------------------------
// <copyright file="HomeModel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Threading.Tasks;

  using Microsoft.AspNetCore.Mvc.Rendering;
  using Microsoft.EntityFrameworkCore;

  public class HomeModel : AppDataModel
  {
    #region Public Properties

    [Required]
    public int ScopeId { get; set; }

    public int PriceLevelPrimaryID { get; set; }

    public int PriceLevelSecondaryID { get; set; }

    public string LanguageCode { get; set; }

    public string LayoutCode { get; set; }

    public string Name { get; set; }

    public List<SelectListItem> Scopes { get; set; }

    public List<SelectListItem> PriceLevelPrimary { get; set; }

    public List<SelectListItem> PriceLevelSecondary { get; set; }

    public List<SelectListItem> Languages { get; set; }

    public List<SelectListItem> Layouts { get; set; }

    #endregion

    #region Public Methods

    public override void Execute()
    {
      LanguageCode = Settings.CatalogLanguages.Values.FirstOrDefault()?.Code;
      LayoutCode = Settings.CatalogLayouts.Values.FirstOrDefault()?.Code;
    }

    public override async Task OnCreatingViewResultAsync()
    {
      Scopes = await DataContext.Hierarchy.OrderBy(e => e.Path).Select(e => new SelectListItem
      {
        Text = e.Path,
        Value = e.Idlevel.ToString(),
      }).ToListAsync();

      PriceLevelPrimary = await DataContext.PriceLevel.OrderBy(e => e.IntIdpriceList).Select(e => new SelectListItem
      {
        Text = e.StrTextId,
        Value = e.IntIdpriceList.ToString(),
        Selected = e.IntIdpriceList == 1,
      }).ToListAsync();

      PriceLevelPrimary.Add(new SelectListItem(Settings.GetCatalogLanguage(Settings.AppLanguageCode)?.NoPrices, "-1"));

      PriceLevelSecondary = await DataContext.PriceLevel.OrderBy(e => e.IntIdpriceList).Select(e => new SelectListItem
      {
        Text = e.StrTextId,
        Value = e.IntIdpriceList.ToString(),
      }).ToListAsync();

      PriceLevelSecondary.Add(new SelectListItem(Settings.GetCatalogLanguage(Settings.AppLanguageCode)?.NoPrices, "-1"));
      PriceLevelSecondary.First(e => e.Value == "-1").Selected = true;

      Languages = Settings.CatalogLanguages.Values.Select(i => new SelectListItem
      {
        Text = i.Name,
        Value = i.Code,
      }).ToList();

      Layouts = Settings.CatalogLayouts.Values.Select(i => new SelectListItem
      {
        Text = i.Name,
        Value = i.Code,
      }).ToList();
    }

    #endregion
  }
}
