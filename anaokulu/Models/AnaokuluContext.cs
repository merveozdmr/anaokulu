namespace anaokulu.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AnaokuluContext : DbContext
    {
        public AnaokuluContext()
            : base("name=AnaokuluContext2")
        {
        }

        public virtual DbSet<Etkinlik> Etkinlik { get; set; }
        public virtual DbSet<EtkinlikResim> EtkinlikResim { get; set; }
        public virtual DbSet<Haber> Haber { get; set; }
        public virtual DbSet<HaberResim> HaberResim { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<Ogrenci> Ogrenci { get; set; }
        public virtual DbSet<Ogretmen> Ogretmen { get; set; }
        public virtual DbSet<Sinif> Sinif { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Yetki> Yetki { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
