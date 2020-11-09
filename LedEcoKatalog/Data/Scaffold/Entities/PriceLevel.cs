using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LedEcoKatalog.Data.Scaffold.Entities
{
    public partial class PriceLevel
    {
        [Column("intIDPriceList")]
        public int IntIdpriceList { get; set; }
        [Column("strTextID")]
        [StringLength(255)]
        public string StrTextId { get; set; }
    }
}
