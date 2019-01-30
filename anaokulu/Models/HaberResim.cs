namespace anaokulu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HaberResim")]
    public partial class HaberResim
    {
        public int haberResimID { get; set; }

        public int? haberID { get; set; }

        [StringLength(50)]
        public string resim { get; set; }

        [StringLength(100)]
        public string aciklama { get; set; }

        public virtual Haber Haber { get; set; }
    }
}
