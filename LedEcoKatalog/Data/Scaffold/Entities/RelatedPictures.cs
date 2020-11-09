using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedEcoKatalog.Data.Scaffold.Entities
{
    public partial class RelatedPictures
    {
        [Column("intIDISObject")]
        public int IntIdisobject { get; set; }
        [Column("strDescription")]
        [StringLength(255)]
        public string StrDescription { get; set; }
        [Column("img", TypeName = "image")]
        public byte[] Img { get; set; }
    }
}
