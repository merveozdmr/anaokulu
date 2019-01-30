using anaokulu.Filtre;
using anaokulu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulu.Controllers
{

 
    public class LoginController : Controller
    {
        // GET: Login
        AnaokuluContext ctx = new AnaokuluContext();

     
        public ActionResult Index()
        {
            return View();
        }

       
        [HttpPost]
    
        public ActionResult Index(string eposta, string sifre)
        {
            
            Kullanici k = ctx.Kullanici.Where(x => x.eposta == eposta && x.sifre == sifre).SingleOrDefault();
            //Ogretmen o = db.Ogretmen.Where(x => x.eposta == eposta && x.sifre == sifre).SingleOrDefault();
            if (k == null)
            {// veritabanında yoksa
                ViewBag.Sonuc = "Kullanıcı bulunamadı.";
                return View();
            }
            else
            {
                Session["Kullanici"] = k;
                return RedirectToAction("Index2", "Anasayfa");
            }
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }
        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifremiUnuttum(string eposta)
        {
            Kullanici kullanici = ctx.Kullanici.Where(x => x.eposta == eposta).SingleOrDefault();
            if (kullanici != null)
            {
                string konu = "Şifre Hatırlatma";
                string mesaj = "Yeni Şifreniz :" + kullanici.sifre;
                Eposta.Gonder(kullanici.eposta, konu, mesaj);
                ViewBag.Uyari = "Epostanıza şifreniz gönderilmiştir. Lütfen Epostanızı kontrol ediniz!";
            }
            else
            {
                ViewBag.Uyarı = "Böyle bir Eposta adresi bulunamadı !";
            }
            return View();
        }
    }
}