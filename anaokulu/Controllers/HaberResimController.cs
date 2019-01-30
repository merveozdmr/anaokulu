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
    public class HaberResimController : Controller
    {
        AnaokuluContext ctx = new AnaokuluContext();
        public ActionResult HaberResim()
        {
            List<HaberResim> resim = ctx.HaberResim.ToList();

            return View(resim);
        }
        public ActionResult Haberimizresim(int id)
        {
            List<HaberResim> resim = ctx.HaberResim.Where(x => x.haberID == id).ToList();

            return View(resim);
        }

        public ActionResult HaberResimEkle()
        {
            ViewBag.Kullanici = ctx.Kullanici.ToList();
            ViewBag.Haber = ctx.Haber.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult HaberResimEkle(HaberResim hr, HttpPostedFileBase resimGelen)
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
                res.Save(Server.MapPath("/Content/Tema/haberresim/") + resimGelen.FileName);
                hr.resim = resimGelen.FileName;

                //string name = "/Content/Tema/kresim/" + Guid.NewGuid() + Path.GetExtension(resimGelen.FileName);

                //Bitmap res = new Bitmap(orjinalResim, width, height);
                //res.Save(Server.MapPath(name) + resimGelen.FileName);
                //k.resim = resimGelen.FileName;
            }

            ctx.HaberResim.Add(hr);
            ctx.SaveChanges();
            return RedirectToAction("HaberResim", "HaberResim");
        }

        [HttpPost]
        public ActionResult HaberResimSil(int id)
        {
            HaberResim hr = ctx.HaberResim.FirstOrDefault(x => x.haberResimID == id);
            ctx.HaberResim.Remove(hr);
            ctx.SaveChanges();
            return RedirectToAction("Index", "HaberResim");
        }
    }
}