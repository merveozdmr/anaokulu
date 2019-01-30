using anaokulu.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulu.Controllers
{
  
    public class HaberController : Controller
    {
        // GET: Haber
        AnaokuluContext ctx =new AnaokuluContext();
        public ActionResult Index()
        {
            List<Haber> haberler = ctx.Haber.ToList();
            return View(haberler);
        }
        public ActionResult Haberler()
        {
            List<Haber> haberler = ctx.Haber.ToList();
            return View(haberler);
        }
        public ActionResult HaberDetay(int id)
        {
            List<Haber> detay = ctx.Haber.Where(x => x.haberID == id).ToList();
            return View(detay);
        }
        public ActionResult HaberEkle()
        {
            ViewBag.Kullanici = ctx.Kullanici.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult HaberEkle(Haber h, HttpPostedFileBase resimGelen)
        {
            if (resimGelen != null)
            {
                Image orjinalResim = Image.FromStream(resimGelen.InputStream);
                int width = Convert.ToInt32(ConfigurationManager.AppSettings["KRW"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["KRH"].ToString());



                Bitmap res = new Bitmap(orjinalResim, width, height);
                res.Save(Server.MapPath("/Content/Tema/haberresim/") + resimGelen.FileName);
                h.kapakResmi = resimGelen.FileName;

                //string name = "/Content/Tema/kresim/" + Guid.NewGuid() + Path.GetExtension(resimGelen.FileName);

                //Bitmap res = new Bitmap(orjinalResim, width, height);
                //res.Save(Server.MapPath(name) + resimGelen.FileName);
                //k.resim = resimGelen.FileName;
            }
            ctx.Haber.Add(h);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Haber");
        }

        [HttpPost]
        public string HaberSil(int id)
        {
            Haber h = ctx.Haber.FirstOrDefault(x => x.haberID == id);
            ctx.Haber.Remove(h);
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

        public ActionResult HaberGuncelle(int id)
        {
            Haber h = (from a in ctx.Haber where a.haberID == id select a).FirstOrDefault();
            ViewBag.Kullanici = ctx.Kullanici.ToList();
            ViewBag.Kullanici = new SelectList(ctx.Kullanici.ToList(), "kullaniciID", "ad", h.kullaniciID);

            return View(h);
        }
        [HttpPost]
        public ActionResult HaberGuncelle(Haber h)
        {
            Haber mevcut = (from a in ctx.Haber where a.haberID == h.haberID select a).FirstOrDefault();
            ViewBag.Kullanici = ctx.Kullanici.ToList();
            ViewBag.Kullanici = new SelectList(ctx.Kullanici.ToList(), "kullaniciID", "ad", h.kullaniciID);

            mevcut.haberID = h.haberID;
            mevcut.baslik = h.baslik;
            mevcut.icerik = h.icerik;
            mevcut.tarih = h.tarih;
            mevcut.kapakResmi = h.kapakResmi;
            
            mevcut.kullaniciID = h.kullaniciID;
            ctx.SaveChanges();
            return RedirectToAction("Index", new { id = h.haberID });
        }

        public ActionResult KullaniciPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/HaberListesi.pdf")
            };
        }
    }
}