namespace anaokulu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EtkinlikResim")]
    public partial class EtkinlikResim
    {
        public int etkinlikResimID { get; set; }

        public int? etkinlikID { get; set; }

        [StringLength(50)]
        public string resim { get; set; }

        [StringLength(500)]
        public string aciklama { get; set; }

        public virtual Etkinlik Etkinlik { get; set; }
    }
}
