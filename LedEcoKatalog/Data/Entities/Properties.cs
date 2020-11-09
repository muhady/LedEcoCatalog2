//----------------------------------------------------------------------------
// <copyright file="Properties.cs" company="Binaware">
//   Copyright (c) Binaware s.r.o. All rights reserved.
// </copyright>
//----------------------------------------------------------------------------

namespace LedEcoKatalog.Data
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  [Table("Properties")]
  public partial class Properties
  {
    #region Public Properties

    [Column("intIDProperty")]
    public int IntIdproperty { get; set; }

    [Column("intIDISObject")]
    public int IntIdisobject { get; set; }

    [Column("intIDLanguageDependant")]
    public int IntIdlanguageDependant { get; set; }

    [Column("strName")]
    [StringLength(100)]
    public string StrName { get; set; }

    [Column("strDescription", TypeName = "ntext")]
    public string StrDescription { get; set; }

    [Column("intIDLanguage")]
    public int IntIdlanguage { get; set; }

    #endregion
  }
}
