//----------------------------------------------------------------------------
// <copyright file="CatalogSection2t.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public partial class CatalogSection2t
  {
    #region Public Properties

    [Column("numValue")]
    public double? NumValue { get; set; }

    [Column("intIDProductBase")]
    public int IntIdproductBase { get; set; }

    [Column("Kód")]
    [StringLength(255)]
    public string Kód { get; set; }

    [Column("Názov")]
    [StringLength(4000)]
    public string Názov { get; set; }

    [Column("CCT (K)")]
    [StringLength(4000)]
    public string CctK { get; set; }

    [Column("Šírka")]
    public double? Šírka { get; set; }

    [Column("Dĺžka")]
    public double? Dĺžka { get; set; }

    [Column("Výška")]
    public double? Výška { get; set; }

    [Column("Poradie")]
    public double? Poradie { get; set; }

    [Column("Popis")]
    [StringLength(255)]
    public string Popis { get; set; }

    [Column("Popis dlhý 1")]
    public string PopisDlhý1 { get; set; }

    [Column("Popis dlhý 2")]
    public string PopisDlhý2 { get; set; }

    [Column("Obrázok")]
    [StringLength(255)]
    public string Obrázok { get; set; }

    [Column("Hmotnosť")]
    public double? Hmotnosť { get; set; }

    [Column("Skladom")]
    public double? Skladom { get; set; }

    [Column("Skladová jednotka")]
    [StringLength(20)]
    public string SkladováJednotka { get; set; }

    [Column("strText1")]
    [StringLength(4000)]
    public string StrText1 { get; set; }

    [Column("strText2")]
    [StringLength(4000)]
    public string StrText2 { get; set; }

    [Column("Vyrobca")]
    [StringLength(4000)]
    public string Vyrobca { get; set; }

    [Column("Kategória")]
    [StringLength(4000)]
    public string Kategória { get; set; }

    [Column("Level")]
    public int Level { get; set; }

    [Column("Page")]
    public long? Page { get; set; }

    #endregion
  }
}
