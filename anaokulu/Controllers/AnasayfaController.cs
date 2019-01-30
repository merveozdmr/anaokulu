using anaokulu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulu.Controllers
{
    public class AnasayfaController : Controller
    {
        AnaokuluContext ctx = new AnaokuluContext();
        // GET: Anasayfa

        
        public ActionResult Index()
        {
            ViewModel vm = new ViewModel();
            vm.Kullanici= ctx.Kullanici.ToList();
            vm.Etkinlik = ctx.Etkinlik.ToList();
            vm.Sinif = ctx.Sinif.ToList();
            
            vm.EtkinlikResim = ctx.EtkinlikResim.ToList();
            vm.Haber = ctx.Haber.ToList();
            vm.HaberResim = ctx.HaberResim.ToList();

            return View(vm);
        }
        public ActionResult Index2()
        {
            List<Kullanici> kullanici = ctx.Kullanici.ToList();

            return View(kullanici);
        }
        public ActionResult Profilim(int id)
        {
            List<Kullanici> kullanici = ctx.Kullanici.Where(x=>x.kullaniciID==id).ToList();
            return View(kullanici);
        }
     
        public ActionResult Iletisim()
        {
            return View();
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }
    }
}