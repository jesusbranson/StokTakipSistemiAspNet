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
    public class KategoriController : Controller
    {
        Entities db = new Entities();

        public ActionResult Kategoriler(int sayfa=1)
        {
           
            var Kategoriler = db.tbl_Kategoriler.ToList().ToPagedList(sayfa,4);
            
           

            return View(Kategoriler);
        }
        
        public ActionResult KategorilerAra (string p)
        {
            var degerler = from i in db.tbl_Kategoriler select i;

            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(v => v.kategoriAD.Contains(p));
            }
            return View(degerler.ToList());
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }


        [HttpPost]
        public ActionResult YeniKategori( tbl_Kategoriler veri)
        {
            if(!ModelState.IsValid) // doğrulama işlemi yapılmadıysa isvalid= doğrulama işlemi yapıldıysa !isvalid = yapılmadıysa
            {
                return View("YeniKategori");
            }
            db.tbl_Kategoriler.Add(veri);
            db.SaveChanges();

            return RedirectToAction("Kategoriler");
        }
        public ActionResult Sil(short? veri)
        {
            var ürünSil = db.tbl_Kategoriler.Find(veri);
            db.tbl_Kategoriler.Remove(ürünSil);
            db.SaveChanges();
            return RedirectToAction("Kategoriler");   
        }
        public ActionResult GüncelleSayfası(int veri)
        {
            var Kategori = db.tbl_Kategoriler.Find(veri);
            return View("GüncelleSayfası", Kategori);
        }
        [HttpPost]
        public ActionResult Güncelle(tbl_Kategoriler veri)
        {
            var kategoriKontrol = db.tbl_Kategoriler.Find(veri.kategoriID);
            kategoriKontrol.kategoriAD = veri.kategoriAD;
            db.SaveChanges();
            return RedirectToAction("Kategoriler");   
        }
    }
}