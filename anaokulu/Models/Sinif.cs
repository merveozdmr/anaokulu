namespace anaokulu.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sinif")]
    public partial class Sinif
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sinif()
        {
            Etkinlik = new HashSet<Etkinlik>();
            Ogrenci = new HashSet<Ogrenci>();
        }

        public int sinifID { get; set; }

        [StringLength(50)]
        public string ad { get; set; }

        [StringLength(300)]
        public string aciklama { get; set; }

        [StringLength(50)]
        public string resim { get; set; }

        public int? ogretmenID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Etkinlik> Etkinlik { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ogrenci> Ogrenci { get; set; }

        public virtual Ogretmen Ogretmen { get; set; }
    }
}
