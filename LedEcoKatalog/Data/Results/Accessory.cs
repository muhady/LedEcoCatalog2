//----------------------------------------------------------------------------
// <copyright file="Accessory.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public partial class Accessory
  {
    #region Public Properties

    [Column("Level")]
    public int Level { get; set; }

    [Column("Kategória")]
    [StringLength(4000)]
    public string Kategória { get; set; }

    [Column("Page")]
    public long? Page { get; set; }

    [Column("intIDProductBase")]
    public int IntIdproductBase { get; set; }

    [Column("RN")]
    public long? Rn { get; set; }

    [Column("intIDProduct")]
    public int IntIdproduct { get; set; }

    [Column("Kod")]
    [StringLength(255)]
    public string Code { get; set; }

    [Column("intIDCoupledProduct")]
    public int IntIdcoupledProduct { get; set; }

    [Column("Popis")]
    [StringLength(255)]
    public string Description { get; set; }

    [Column("Nazov")]
    [StringLength(4000)]
    public string Name { get; set; }

    [Column("numValue")]
    public double? Price { get; set; }

    [Column("img")]
    public byte[] Image { get; set; }

    #endregion
  }
}
