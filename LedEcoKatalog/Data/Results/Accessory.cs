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
    public string Kod { get; set; }

    [Column("intIDCoupledProduct")]
    public int IntIdcoupledProduct { get; set; }

    [Column("Popis")]
    [StringLength(255)]
    public string Popis { get; set; }

    [Column("Nazov")]
    [StringLength(4000)]
    public string Nazov { get; set; }

    [Column("numValue")]
    public double? NumValue { get; set; }

    [Column("img")]
    public byte[] Img { get; set; }

    #endregion
  }
}
