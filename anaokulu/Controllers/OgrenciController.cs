using anaokulu.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulu.Controllers
{
    
    public class OgrenciController : Controller
    {
        AnaokuluContext ctx = new AnaokuluContext();
        // GET: Ogrenci
        public ActionResult Index()
        {
            List<Ogrenci> ogrenci = ctx.Ogrenci.ToList();
            return View(ogrenci);
        }
       
        public ActionResult OgrenciEkle()
        {
            ViewBag.Sinif = ctx.Sinif.ToList();
            return View();
        }
        
        [HttpPost]
        public ActionResult OgrenciEkle(Ogrenci ogrenci)
        {
            ViewBag.Sinif = ctx.Sinif.ToList();
            ctx.Ogrenci.Add(ogrenci);
            ctx.SaveChanges();
            return RedirectToAction("Index","Ogrenci");
        }

        public ActionResult OgrenciGuncelle(int id)
        {
            Ogrenci ogrenci = (from a in ctx.Ogrenci where a.ogrenciID == id select a).FirstOrDefault();
            ViewBag.Sinif = ctx.Sinif.ToList();
            ViewBag.Sinif = new SelectList(ctx.Sinif.ToList(), "sinifID", "ad", ogrenci.sinifID);

            return View(ogrenci);
        }
        [HttpPost]
        public ActionResult OgrenciGuncelle(Ogrenci ogrenci)
        {
            Ogrenci mevcut = (from a in ctx.Ogrenci where a.ogrenciID == ogrenci.ogrenciID select a).FirstOrDefault();
            ViewBag.Sinif = new SelectList(ctx.Sinif.ToList(), "sinifID", "ad", ogrenci.sinifID);
            ViewBag.Sinif = ctx.Sinif.ToList();
            mevcut.ogrenciID = ogrenci.ogrenciID;
            mevcut.ad = ogrenci.ad;
            mevcut.soyad = ogrenci.soyad;
            mevcut.dogumTarihi = ogrenci.dogumTarihi;
            mevcut.cinsiyet = ogrenci.cinsiyet;
            mevcut.sinifID = ogrenci.sinifID;
            ctx.SaveChanges();
            return RedirectToAction("Index", new { id = ogrenci.ogrenciID });
        }

        [HttpPost]
        public string OgrenciSil(int id)
        {
            Ogrenci ogrenci = ctx.Ogrenci.FirstOrDefault(x => x.ogrenciID == id);
            ctx.Ogrenci.Remove(ogrenci);
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
        public ActionResult OgrenciPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/OgrenciListesi.pdf")
            };
        }
    }
}