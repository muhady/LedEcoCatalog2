//----------------------------------------------------------------------------
// <copyright file="PageInfo.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public partial class PageInfo
  {
    #region Public Properties

    [Column("PathToCategory")]
    [StringLength(1000)]
    public string PathToCategory { get; set; }

    [Column("ParentCategoryName")]
    [StringLength(255)]
    public string ParentCategoryName { get; set; }

    [Column("CategoryName")]
    [StringLength(255)]
    public string CategoryName { get; set; }

    [Column("Level")]
    public int Level { get; set; }

    [Column("Kategória")]
    [StringLength(4000)]
    public string Kategória { get; set; }

    [Column("Page")]
    public long? Number { get; set; }

    [Column("Kratky popis kategorie")]
    public string KratkyPopisKategorie { get; set; }

    [Column("BannerImage")]
    public byte[] BannerImage { get; set; }

    [Column("PictureImage")]
    public byte[] PictureImage { get; set; }

    [Column("Popis produktu Parent")]
    public string PopisProduktuParent { get; set; }

    [Column("strText9")]
    [StringLength(4000)]
    public string StrText9 { get; set; }

    [Column("strText10")]
    [StringLength(4000)]
    public string StrText10 { get; set; }

    #endregion
  }
}
