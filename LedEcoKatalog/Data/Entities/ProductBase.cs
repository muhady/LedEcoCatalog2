//----------------------------------------------------------------------------
// <copyright file="ProductBase.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("ProductBase")]
  public partial class ProductBase
  {
    #region Public Properties

    [Column("intIDProductBase")]
    public int IntIdproductBase { get; set; }

    [Column("strCatalogueNumber")]
    [StringLength(255)]
    public string StrCatalogueNumber { get; set; }

    public byte? Type { get; set; }

    public byte? Kind { get; set; }

    [Column("numGuarantee")]
    public double? NumGuarantee { get; set; }

    [Column("numDeliveryTime")]
    public double? NumDeliveryTime { get; set; }

    [Column("numPiecesInPackage")]
    public double? NumPiecesInPackage { get; set; }

    [Column("bitAlwaysSeparatePack")]
    public bool? BitAlwaysSeparatePack { get; set; }

    [Column("bitAllInOnePack")]
    public bool? BitAllInOnePack { get; set; }

    [Required]
    [Column("_TIMESTAMP")]
    public byte[] Timestamp { get; set; }

    [Column("datDate1", TypeName = "datetime")]
    public DateTime? DatDate1 { get; set; }

    [Column("datDate2", TypeName = "datetime")]
    public DateTime? DatDate2 { get; set; }

    [Column("datDate3", TypeName = "datetime")]
    public DateTime? DatDate3 { get; set; }

    [Column("numNumber1")]
    public double? NumNumber1 { get; set; }

    [Column("numNumber2")]
    public double? NumNumber2 { get; set; }

    [Column("numNumber3")]
    public double? NumNumber3 { get; set; }

    [Column("numNumber4")]
    public double? NumNumber4 { get; set; }

    [Column("numNumber5")]
    public double? NumNumber5 { get; set; }

    [Column("ro")]
    public long? Ro { get; set; }

    #endregion
  }
}
