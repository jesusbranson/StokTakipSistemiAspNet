using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _50DersMvc.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace _50DersMvc.Controllers
{
    public class MüşteriController : Controller
    {
        Entities db = new Entities();

        public ActionResult Müşteriler(int sayfa=1)
        {
            var müsteriler = db.tbl_Müsteriler.ToList().ToPagedList(sayfa,3);
            return View(müsteriler);
        }
        public ActionResult MüsteriAra(string p)
        {
            var degerler = from i in db.tbl_Müsteriler select i;

            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(v => v.müsteriAD.Contains(p));
            }
            return View(degerler.ToList());
        }
        [HttpGet]
        public ActionResult MüşterilerEkle()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult MüşterilerEkle(tbl_Müsteriler veri)
        {
            if(!ModelState.IsValid)
            {
                return View("MüşterilerEkle");
            }
            var Müsteriler = db.tbl_Müsteriler.Add(veri);
            db.SaveChanges();
            return RedirectToAction("Müşteriler");
        }
        public ActionResult Sil (int veri)
        {

            var MüsteriSil = db.tbl_Müsteriler.Find(veri);
            db.tbl_Müsteriler.Remove(MüsteriSil);
            db.SaveChanges();
            return RedirectToAction("Müşteriler");
        }
        public ActionResult GüncelleSayfa (int veri)
        {
            var Güncelle = db.tbl_Müsteriler.Find(veri);
            return View("GüncelleSayfa",Güncelle);
        }
        public ActionResult Güncelle (tbl_Müsteriler veri)
        {
            var Müsteri = db.tbl_Müsteriler.Find(veri.müsteriID);
            Müsteri.müsteriAD = veri.müsteriAD;
            Müsteri.müsteriID = veri.müsteriID;
            Müsteri.müsteriSOYAD = veri.müsteriSOYAD;
            db.SaveChanges();
            return RedirectToAction("Müşteriler");
        }
    }
}