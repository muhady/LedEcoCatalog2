//----------------------------------------------------------------------------
// <copyright file="PriceLevel.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("PriceLevel")]
  public partial class PriceLevel
  {
    #region Public Properties

    [Column("intIDPriceList")]
    public int IntIdpriceList { get; set; }

    [Column("strTextID")]
    [StringLength(255)]
    public string StrTextId { get; set; }

    #endregion
  }
}
