//----------------------------------------------------------------------------
// <copyright file="PageModel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using System.Collections.Generic;

  using LedEcoKatalog.Data;

  public class PageModel
  {
    #region Public Properties

    public PageInfo PageInfo { get; set; }

    public List<Product> Products { get; set; }

    public List<ProductPicture> ProductPictures { get; set; }

    public List<CatalogSection2pic3rdt> Section2Pic3Rdts { get; set; }

    public List<Accessory> Accessories { get; set; }

    public List<LegendItem> LegendItems { get; set; }

    public string Language { get; set; }

    public int PriceLevel { get; set; }

    public CatalogLanguage CatalogLanguage { get; set; }

    public string Style { get; set; }

    #endregion
  }
}
