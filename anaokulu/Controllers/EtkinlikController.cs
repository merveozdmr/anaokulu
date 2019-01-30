using anaokulu.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulu.Controllers
{
  
    public class EtkinlikController : Controller
    {
        AnaokuluContext ctx = new AnaokuluContext();
        // GET: Kullanici
       
        public ActionResult Index(int id)
        {
            List<Etkinlik> etkinlik = ctx.Etkinlik.Where(x => x.sinifID == id).ToList();
            return View(etkinlik);
        }
        public ActionResult Etkinliklerimiz(int id)
        {
            List<Etkinlik> etkinliklerimiz = ctx.Etkinlik.Where(x=>x.sinifID==id).ToList();
            return View(etkinliklerimiz);
        }
        public ActionResult Detay(int id)
        {
            List<Etkinlik> detay = ctx.Etkinlik.Where(x => x.etkinlikID == id).ToList();
            return View(detay);
        }
     
       
        public ActionResult EtkinlikEkle()
        {
            
            ViewBag.Sinif = ctx.Sinif.ToList();
            //var snf = ctx.Sinif.ToList();
            //ViewBag.Sinif = new SelectList(snf, "sinifID", "ad");
            return View();
        }

       
        [HttpPost]
        public ActionResult EtkinlikEkle(Etkinlik e)
        {
            
            ctx.Etkinlik.Add(e);
            ctx.SaveChanges();
            return RedirectToAction("Index","Sinif");
        }

       
        [HttpPost]
        public string EtkinlikSil(int id)
        {
            Etkinlik et = ctx.Etkinlik.FirstOrDefault(x => x.etkinlikID == id);
            ctx.Etkinlik.Remove(et);
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
       
        public ActionResult EtkinlikGuncelle(int id)
        {
            Etkinlik et = (from a in ctx.Etkinlik where a.etkinlikID == id select a).FirstOrDefault();
            ViewBag.Sinif = ctx.Sinif.ToList();
            ViewBag.Sinif = new SelectList(ctx.Sinif.ToList(), "sinifID", "ad", et.sinifID);
            return View(et);
        }
        
        [HttpPost]
        public ActionResult EtkinlikGuncelle(Etkinlik et)
        {
            Etkinlik mevcut = (from a in ctx.Etkinlik where a.etkinlikID == et.etkinlikID select a).FirstOrDefault();
            ViewBag.Sinif = ctx.Sinif.ToList();
            ViewBag.Sinif = new SelectList(ctx.Sinif.ToList(), "sinifID", "ad", et.sinifID);
            mevcut.etkinlikID = et.etkinlikID;
            mevcut.ad = et.ad;
            mevcut.aciklama = et.aciklama;
            mevcut.tarih = et.tarih;
            mevcut.sinifID = et.sinifID;
            ctx.SaveChanges();
            return RedirectToAction("Index","Sinif", new { id = et.etkinlikID });
        }

        public ActionResult EtkinlikPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/EtkinlikListesi.pdf")
            };
        }



        
    }
}