using anaokulu.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulu.Controllers
{
    
    public class OgretmenController : Controller
    {

        AnaokuluContext ctx =new AnaokuluContext();
        // GET: Ogretmen
      
        public ActionResult Index()
        {
            //ViewModel vm = new ViewModel();
            //vm.Kullanici = ctx.Kullanici.ToList();

            List<Ogretmen> ogretmen = ctx.Ogretmen.ToList();
            return View(ogretmen);
        }
        public ActionResult Personellerimiz()
        {
            List<Ogretmen> personel = ctx.Ogretmen.ToList();
            return View(personel);
        }

        public ActionResult OgretmenEkle()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult OgretmenEkle(Ogretmen o)
        {
        
            ctx.Ogretmen.Add(o);
            ctx.SaveChanges();
            return RedirectToAction("Index","Ogretmen");
        }

        [HttpPost]
        public string OgretmenSil(int id)
        {
            Ogretmen ogr = ctx.Ogretmen.FirstOrDefault(x => x.ogretmenID == id);
            ctx.Ogretmen.Remove(ogr);
            try
            {
                ctx.SaveChanges();
                return "başarılı";
            }
            catch (Exception)
            {
                return "hata";
            }

        }

        public ActionResult OgretmenGuncelle(int id)
        {
            Ogretmen ogr = (from a in ctx.Ogretmen where a.ogretmenID == id select a).FirstOrDefault();
            //ViewBag.Yetki = ctx.Yetki.ToList();
            //ViewBag.Yetki = new SelectList(ctx.Yetki.ToList(), "yetkiID", "ad", ku.yetkiID);

            return View(ogr);
        }
        [HttpPost]
        public ActionResult OgretmenGuncelle(Ogretmen ogr)
        {
            Ogretmen mevcut = (from a in ctx.Ogretmen where a.ogretmenID == ogr.ogretmenID select a).FirstOrDefault();
            //ViewBag.Yetki = new SelectList(ctx.Yetki.ToList(), "yetkiID", "ad", ku.yetkiID);
            //ViewBag.Yetki = ctx.Yetki.ToList();
            mevcut.ogretmenID = ogr.ogretmenID;
            mevcut.ad = ogr.ad;
            mevcut.soyad = ogr.soyad;
            mevcut.eposta = ogr.eposta;
            mevcut.sifre = ogr.sifre;
            mevcut.brans = ogr.brans;
            mevcut.brans = ogr.brans;
            mevcut.kayitTarih = ogr.kayitTarih;
            ctx.SaveChanges();
            return RedirectToAction("Index", new { id = ogr.ogretmenID });
        }

        public ActionResult OgretmenPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/OgretmenListesi.pdf")
            };
        }
    }
}