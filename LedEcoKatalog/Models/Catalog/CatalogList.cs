//----------------------------------------------------------------------------
// <copyright file="CatalogList.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Models
{
  using System.Collections.Generic;

  using LedEcoKatalog.Data;

  public class CatalogList
  {
    #region Public Properties

    public CatalogSection1t Sec1 { get; set; }

    public List<CatalogSection2t> Products { get; set; }

    public List<CatalogSection2pict> Section2Picts { get; set; }

    public List<CatalogSection2pic3rdt> Section2Pic3Rdts { get; set; }

    public List<CatalogSection3t> Accessories { get; set; }

    public List<CatalogSection4t> Legends { get; set; }

    public string Language { get; set; }

    public int PriceLevel { get; set; }

    public CatalogLanguage CatalogLanguage { get; set; }

    #endregion
  }
}
