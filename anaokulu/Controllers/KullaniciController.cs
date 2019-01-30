using anaokulu.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulu.Controllers
{
   
    public class KullaniciController : Controller
    {
        AnaokuluContext ctx = new AnaokuluContext();
        
        public ActionResult Index()
        {
            List<Kullanici> kullanici = ctx.Kullanici.ToList();

            return View(kullanici);
        }
        

        public ActionResult KullaniciEkle()
        {
            ViewBag.Yetki = ctx.Yetki.ToList();
            return View();
        }
       
        [HttpPost]
        public ActionResult KullaniciEkle(Kullanici k, HttpPostedFileBase resimGelen)
        {
            if (resimGelen != null)
            {
                Image orjinalResim = Image.FromStream(resimGelen.InputStream);
                int width = Convert.ToInt32(ConfigurationManager.AppSettings["KRW"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["KRH"].ToString());

                //resim isimlerinin aynı olmaması için;
                //guid== 24 basamaklı eşi benzeri olmayan bir string ifade verir
                //resmi jpg diye yazmak yerine resmin kendi uzantısı ne ise onu aldık
                //getExtension dosyanın uzantısını alır.
                //resmin uzantısını ekleyerek yeni isim oluştu
                
                Bitmap res = new Bitmap(orjinalResim,width,height);
                res.Save(Server.MapPath("/Content/Tema/kresim/") + resimGelen.FileName);
                k.resim = resimGelen.FileName;

                //string name = "/Content/Tema/kresim/" + Guid.NewGuid() + Path.GetExtension(resimGelen.FileName);

                //Bitmap res = new Bitmap(orjinalResim, width, height);
                //res.Save(Server.MapPath(name) + resimGelen.FileName);
                //k.resim = resimGelen.FileName;
            }
            //Image orjinalResim = Image.FromStream(resimGelen.InputStream);
            //Bitmap res = new Bitmap(orjinalResim);
            //res.Save(Server.MapPath("/") + resimGelen.FileName);
            //k.resim = resimGelen.FileName;
            ctx.Kullanici.Add(k);
            ctx.SaveChanges();
            return RedirectToAction("Index","Kullanici");
        }
        

        [HttpPost]
        public string KullaniciSil(int id)
        {
            Kullanici ku = ctx.Kullanici.FirstOrDefault(x => x.kullaniciID == id);
            ctx.Kullanici.Remove(ku);
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

        public ActionResult KullaniciGuncelle(int id)
        {
            Kullanici ku = (from a in ctx.Kullanici where a.kullaniciID== id select a).FirstOrDefault();
            ViewBag.Yetki = ctx.Yetki.ToList();
            ViewBag.Yetki = new SelectList(ctx.Yetki.ToList(), "yetkiID", "ad", ku.yetkiID);

            return View(ku);
        }
        [HttpPost]
        public ActionResult KullaniciGuncelle(Kullanici ku)
        {
            Kullanici mevcut = (from a in ctx.Kullanici where a.kullaniciID == ku.kullaniciID select a).FirstOrDefault();
            ViewBag.Yetki = new SelectList(ctx.Yetki.ToList(), "yetkiID", "ad", ku.yetkiID);
            ViewBag.Yetki = ctx.Yetki.ToList();
           
            mevcut.kullaniciID = ku.kullaniciID;
            mevcut.ad = ku.ad;
            mevcut.soyad = ku.soyad;
            mevcut.eposta = ku.eposta;
            mevcut.sifre = ku.sifre;
            mevcut.resim=ku.resim;
            mevcut.yetkiID = ku.yetkiID;
            ctx.SaveChanges();
            return RedirectToAction("Index", new { id = ku.kullaniciID });
        }

        public ActionResult KullaniciPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/KullaniciListesi.pdf")
            };
        }

        

    }
}