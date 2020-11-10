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

    public CatalogLanguage CatalogLanguage { get; set; }

    public Page Page { get; set; }

    public List<LegendItem> LegendItems { get; set; }

    public List<Product> Products { get; set; }

    public List<ProductPicture> ProductPictures { get; set; }

    public List<ProductPicture> ProductPictures2 { get; set; }

    public List<Accessory> Accessories { get; set; }

    public string Style { get; set; }

    public bool HasPrices { get; set; }

    #endregion
  }
}
