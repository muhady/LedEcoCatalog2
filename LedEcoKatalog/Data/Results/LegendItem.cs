//----------------------------------------------------------------------------
// <copyright file="LegendItem.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public partial class LegendItem
  {
    #region Public Properties

    [Column("strPropertyNameForCust")]
    [StringLength(255)]
    public string GroupName { get; set; }

    [Column("val")]
    [StringLength(1000)]
    public string Value { get; set; }

    [Column("intIDProperty")]
    public int? IntIdproperty { get; set; }

    [Column("strExternalID")]
    [StringLength(200)]
    public string ImageFileName { get; set; }

    [Column("Page")]
    public long? Page { get; set; }

    [Column("strName")]
    [StringLength(100)]
    public string Name { get; set; }

    [Column("strDescription")]
    [StringLength(1000)]
    public string Description { get; set; }

    #endregion
  }
}
