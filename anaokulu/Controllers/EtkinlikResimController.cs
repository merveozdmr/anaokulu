using anaokulu.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulu.Controllers
{
 
    public class EtkinlikResimController : Controller
    {
        AnaokuluContext ctx = new AnaokuluContext();
        public ActionResult Resim(int id)
        {
            List<EtkinlikResim> resim = ctx.EtkinlikResim.Where(x => x.etkinlikID == id).ToList();
            
            return View(resim);
        }
        public ActionResult EtkinliklerimizResim(int id)
        {
            List<EtkinlikResim> resim = ctx.EtkinlikResim.Where(x => x.etkinlikID == id).ToList();

            return View(resim);
        }

        public ActionResult ResimEkle()
        {
            ViewBag.Etkinlik = ctx.Etkinlik.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult ResimEkle(EtkinlikResim er, HttpPostedFileBase resimGelen)
        {
            if (resimGelen != null)
            {
                Image orjinalResim = Image.FromStream(resimGelen.InputStream);
                int width = Convert.ToInt32(ConfigurationManager.AppSettings["SRW"].ToString());
                int height = Convert.ToInt32(ConfigurationManager.AppSettings["SRH"].ToString());

                //resim isimlerinin aynı olmaması için;
                //guid== 24 basamaklı eşi benzeri olmayan bir string ifade verir
                //resmi jpg diye yazmak yerine resmin kendi uzantısı ne ise onu aldık
                //getExtension dosyanın uzantısını alır.
                //resmin uzantısını ekleyerek yeni isim oluştu


                Bitmap res = new Bitmap(orjinalResim, width, height);
                res.Save(Server.MapPath("/Content/Tema/etkinlikresim/") + resimGelen.FileName);
                er.resim = resimGelen.FileName;

                //string name = "/Content/Tema/kresim/" + Guid.NewGuid() + Path.GetExtension(resimGelen.FileName);

                //Bitmap res = new Bitmap(orjinalResim, width, height);
                //res.Save(Server.MapPath(name) + resimGelen.FileName);
                //k.resim = resimGelen.FileName;
            }

            ctx.EtkinlikResim.Add(er);
            ctx.SaveChanges();
            return RedirectToAction("Index","Sinif");
        }

        [HttpPost]
        public ActionResult ResimSil(int id)
        {
            EtkinlikResim etk = ctx.EtkinlikResim.FirstOrDefault(x => x.etkinlikResimID == id);
            ctx.EtkinlikResim.Remove(etk);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Sinif");
        }
    }
}