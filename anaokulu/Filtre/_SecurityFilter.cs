using anaokulu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace anaokulu.Filtre
{

    public class _SecurityFilter : ActionFilterAttribute
    {
        string yetkili;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (HttpContext.Current.Session["Kullanici"] == null && ControllerName!="Login"&& ControllerName!="Anasayfa")
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }

            if (HttpContext.Current.Session["Kullanici"] != null)
            {
                Kullanici k = (Kullanici)HttpContext.Current.Session["Kullanici"];
                if (k.yetkiID == 1)
                {
                    yetkili = "müdür";
                }
                if(yetkili!="müdür"&&(ControllerName=="müdür" || ControllerName == "ogretmen"))
                {
                    filterContext.Result = new RedirectResult("/Talep/Index");
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
            //else
            //{
            //    //Kullanici k = (Kullanici)HttpContext.Current.Session["Kullanici"];
            //    //if (k.yetkiID==2 && (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName == "Mudur"))
            //    //{
            //    //    filterContext.Result = new RedirectResult("/Ogretmen/Index");
            //    //    return;
            //    //}
            //    base.OnActionExecuting(filterContext);
            //}
        }
    }
}