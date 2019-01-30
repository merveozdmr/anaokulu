using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anaokulu.Models
{
    public class ViewModel
    {
        public IEnumerable<Kullanici> Kullanici { get; set; }
        public IEnumerable<Etkinlik> Etkinlik { get; set; }
        public IEnumerable<Sinif> Sinif { get; set; }
        public IEnumerable<EtkinlikResim> EtkinlikResim { get; set; }
        public IEnumerable<Haber> Haber { get; set; }
        public IEnumerable<HaberResim> HaberResim { get; set; }
    }
}