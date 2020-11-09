//----------------------------------------------------------------------------
// <copyright file="HomeModel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using System;
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
    public int Scope { get; set; }

    public int PriceLevel { get; set; }

    public string Language { get; set; }

    public string Layout { get; set; }

    public string NameOfCatalog { get; set; }

    public IList<SelectListItem> Hierarchy { get; set; }

    public IList<SelectListItem> PriceLevels { get; set; }

    public IList<SelectListItem> Languages { get; set; }

    public IList<SelectListItem> Layouts { get; set; }

    #endregion

    #region Public Methods

    public override void Execute()
    {
      Language = Settings.CatalogLanguages.Values.FirstOrDefault()?.Code;
      Layout = Settings.CatalogLayouts.Values.FirstOrDefault()?.Code;
    }

    public override async Task OnCreatingViewResultAsync()
    {
      Hierarchy = await DataContext.Hierarchy.Select(e => new SelectListItem
      {
        Text = e.Path,
        Value = e.Idlevel.ToString(),
      }).OrderBy(x => x.Text).ToListAsync();

      var priceLevels = PriceLevels = await DataContext.PriceLevel.Select(e => new SelectListItem
      {
        Text = e.StrTextId,
        Value = e.IntIdpriceList.ToString(),
        Selected = e.IntIdpriceList == 1,
      }).OrderBy(i => Convert.ToInt16(i.Value)).ToListAsync();

      priceLevels.Add(new SelectListItem("Bez ceny", "-1"));

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
