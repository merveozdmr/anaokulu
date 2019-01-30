namespace anaokulu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Etkinlik")]
    public partial class Etkinlik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Etkinlik()
        {
            EtkinlikResim = new HashSet<EtkinlikResim>();
        }

        public int etkinlikID { get; set; }

        [StringLength(200)]
        public string ad { get; set; }

        [StringLength(1000)]
        public string aciklama { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tarih { get; set; }

        public int? sinifID { get; set; }

        public virtual Sinif Sinif { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EtkinlikResim> EtkinlikResim { get; set; }
    }
}
