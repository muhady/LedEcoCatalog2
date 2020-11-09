using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedEcoKatalog.Data.Scaffold.Entities
{
    public partial class Properties
    {
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
    }
}
