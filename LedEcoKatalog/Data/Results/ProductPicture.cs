//----------------------------------------------------------------------------
// <copyright file="ProductPicture.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public partial class ProductPicture
  {
    #region Public Properties

    [Column("Rowinpage")]
    public long? Rowinpage { get; set; }

    [Column("strDescription")]
    [StringLength(255)]
    public string StrDescription { get; set; }

    [Column("img")]
    public byte[] Img { get; set; }

    [Column("intIDISObject")]
    public int IntIdisobject { get; set; }

    [Column("blobData2")]
    public byte[] BlobData2 { get; set; }

    [Column("Level")]
    public int Level { get; set; }

    [Column("Kategória")]
    [StringLength(4000)]
    public string Kategória { get; set; }

    [Column("RN")]
    public long? Rn { get; set; }

    [Column("Page")]
    public long? Page { get; set; }

    #endregion
  }
}
