//----------------------------------------------------------------------------
// <copyright file="RelatedPictures.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("RelatedPictures")]
  public partial class RelatedPictures
  {
    #region Public Properties

    [Column("intIDISObject")]
    public int IntIdisobject { get; set; }

    [Column("strDescription")]
    [StringLength(255)]
    public string StrDescription { get; set; }

    [Column("img", TypeName = "image")]
    public byte[] Img { get; set; }

    #endregion
  }
}
