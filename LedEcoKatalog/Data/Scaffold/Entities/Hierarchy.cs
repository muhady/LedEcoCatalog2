using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedEcoKatalog.Data.Scaffold.Entities
{
    public partial class Hierarchy
    {
        [StringLength(1000)]
        public string Path { get; set; }
        [Column("intIDLanguage")]
        public int? IntIdlanguage { get; set; }
        [Column("intOrdering")]
        public int? IntOrdering { get; set; }
        public int? Level { get; set; }
        [Column("IDLevel")]
        public int? Idlevel { get; set; }
        [Column("IDLevelParent")]
        public int? IdlevelParent { get; set; }
        [StringLength(255)]
        public string Názov { get; set; }
        [Column(TypeName = "ntext")]
        public string Popis { get; set; }
        [StringLength(1000)]
        public string Banner { get; set; }
        [StringLength(255)]
        public string Thumbnail { get; set; }
        [StringLength(255)]
        public string Picture { get; set; }
        [Column("BannerID")]
        [StringLength(50)]
        public string BannerId { get; set; }
        [Column("ThumbnailID")]
        [StringLength(40)]
        public string ThumbnailId { get; set; }
        [Column("PictureID")]
        [StringLength(40)]
        public string PictureId { get; set; }
    }
}
