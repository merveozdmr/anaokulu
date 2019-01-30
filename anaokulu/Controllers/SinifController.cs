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
 // [Authorize]
    public class SinifController : Controller
    {
        AnaokuluContext ctx = new AnaokuluContext();
        // GET: Kullanici
      
        public ActionResult Index()
        {
            List<Sinif> sinif = ctx.Sinif.ToList();

            return View(sinif);
        }

        public ActionResult Siniflarimiz()
        {
            List<Sinif> siniflarimiz = ctx.Sinif.ToList();

            return View(siniflarimiz);
        }
        public ActionResult SinifEkle()
        {
            ViewBag.Ogretmen = ctx.Ogretmen.ToList();
            return View();
        }
       
        [HttpPost]
        public ActionResult SinifEkle(Sinif s,HttpPostedFileBase resimGelen)
        {
            Image orjinalResim = Image.FromStream(resimGelen.InputStream);
            int width = Convert.ToInt32(ConfigurationManager.AppSettings["SRW"].ToString());
            int height = Convert.ToInt32(ConfigurationManager.AppSettings["SRH"].ToString());
            Bitmap res = new Bitmap(orjinalResim,width,height);
            res.Save(Server.MapPath("/Content/Tema/sresim/") + resimGelen.FileName);
            s.resim = resimGelen.FileName;
            ctx.Sinif.Add(s);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Sinif");
        }
        
        [HttpPost]
        public string SinifSil(int id)
        {
            Sinif s = ctx.Sinif.FirstOrDefault(x => x.sinifID == id);
            ctx.Sinif.Remove(s);
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
       
        public ActionResult SinifGuncelle(int id)
        {
            Sinif s = (from a in ctx.Sinif where a.sinifID == id select a).FirstOrDefault();
            ViewBag.Ogretmen = ctx.Ogretmen.ToList();
            ViewBag.Ogretmen = new SelectList(ctx.Ogretmen.ToList(), "ogretmenID", "ad", s.ogretmenID);

            return View(s);
        }
      
        [HttpPost]
        public ActionResult SinifGuncelle(Sinif s)
        {
            Sinif mevcut = (from a in ctx.Sinif where a.sinifID == s.sinifID select a).FirstOrDefault();
            ViewBag.Ogretmen = new SelectList(ctx.Ogretmen.ToList(), "ogretmenID", "ad", s.ogretmenID);
            ViewBag.Ogretmen = ctx.Ogretmen.ToList();
            mevcut.sinifID = s.sinifID;
            mevcut.ad = s.ad;
            mevcut.resim = s.resim;
            mevcut.ogretmenID = s.ogretmenID;
            mevcut.aciklama = s.aciklama;
            ctx.SaveChanges();
            return RedirectToAction("Index", new { id = s.sinifID });
        }


        public ActionResult SinifPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/SinifListesi.pdf")
            };
        }
    }
}