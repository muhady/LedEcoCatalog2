﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedEcoKatalog.Data.Scaffold.Entities
{
    public partial class HierarchyFlatten
    {
        [Column("ordr", TypeName = "numeric(38, 0)")]
        public decimal? Ordr { get; set; }
        [Column("id_01")]
        public int? Id01 { get; set; }
        [Column("name_01")]
        [StringLength(255)]
        public string Name01 { get; set; }
        [Column("lev01ord")]
        public int? Lev01ord { get; set; }
        [Column("intIDLanguage01")]
        public int? IntIdlanguage01 { get; set; }
        [Column("lev01")]
        public int? Lev01 { get; set; }
        [Column("id_02")]
        public int? Id02 { get; set; }
        [Column("name_02")]
        [StringLength(255)]
        public string Name02 { get; set; }
        [Column("lev02ord")]
        public int? Lev02ord { get; set; }
        [Column("intIDLanguage02")]
        public int? IntIdlanguage02 { get; set; }
        [Column("lev02")]
        public int? Lev02 { get; set; }
        [Column("id_03")]
        public int? Id03 { get; set; }
        [Column("name_03")]
        [StringLength(255)]
        public string Name03 { get; set; }
        [Column("lev03ord")]
        public int? Lev03ord { get; set; }
        [Column("intIDLanguage03")]
        public int? IntIdlanguage03 { get; set; }
        [Column("lev03")]
        public int? Lev03 { get; set; }
        [Column("id_04")]
        public int? Id04 { get; set; }
        [Column("name_04")]
        [StringLength(255)]
        public string Name04 { get; set; }
        [Column("lev04ord")]
        public int? Lev04ord { get; set; }
        [Column("intIDLanguage04")]
        public int? IntIdlanguage04 { get; set; }
        [Column("lev04")]
        public int? Lev04 { get; set; }
        [Column("id_05")]
        public int? Id05 { get; set; }
        [Column("name_05")]
        [StringLength(255)]
        public string Name05 { get; set; }
        [Column("lev05ord")]
        public int? Lev05ord { get; set; }
        [Column("intIDLanguage05")]
        public int? IntIdlanguage05 { get; set; }
        [Column("lev05")]
        public int? Lev05 { get; set; }
        [Column("id_06")]
        public int? Id06 { get; set; }
        [Column("name_06")]
        [StringLength(255)]
        public string Name06 { get; set; }
        [Column("lev06ord")]
        public int? Lev06ord { get; set; }
        [Column("intIDLanguage06")]
        public int? IntIdlanguage06 { get; set; }
        [Column("lev06")]
        public int? Lev06 { get; set; }
    }
}
