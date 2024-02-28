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
    public class ÜrünlerController : Controller
    {
        Entities db = new Entities();
        public ActionResult Ürünler(int sayfa=1)
        {
            var Ürünler = db.tbl_Ürünler.ToList().ToPagedList(sayfa,5);
            return View(Ürünler);
        }
        public ActionResult ÜrünAra(string p)
        {
            var degerler = from i in db.tbl_Ürünler select i;

            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(v => v.ürünAD.Contains(p));
            }
            return View(degerler.ToList());
        }

        [HttpGet]
        public ActionResult ÜrünlerEkle()
        {
            List<SelectListItem> degerler = (from i in db.tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.kategoriAD,
                                                 Value = i.kategoriID.ToString()
                                             }).ToList();
            ViewBag.veri = degerler;                           
            return View();
        }
        [HttpPost]
        public ActionResult ÜrünlerEkle(tbl_Ürünler veri)
        {
            var kategoriVeri = db.tbl_Kategoriler.Where(v => v.kategoriID == veri.tbl_Kategoriler.kategoriID).FirstOrDefault();
            veri.tbl_Kategoriler = kategoriVeri;
            db.tbl_Ürünler.Add(veri);
            db.SaveChanges();

            return RedirectToAction("Ürünler");
        }
        public ActionResult Sil(int veri)
        {
            var ürünSil = db.tbl_Ürünler.Find(veri);
            db.tbl_Ürünler.Remove(ürünSil);
            db.SaveChanges();
            return RedirectToAction("Ürünler");
        }
        public ActionResult GüncelleSayfası(int veri)
        {
            List<SelectListItem> degerler = (from i in db.tbl_Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.kategoriAD,
                                                 Value = i.kategoriID.ToString()
                                             }).ToList();
            ViewBag.veri = degerler;
            var GüncelleVeri = db.tbl_Ürünler.Find(veri);
            return View("GüncelleSayfası",GüncelleVeri);   

        }
        public ActionResult Güncelle(tbl_Ürünler veri)
        {
            
            var ürünGüncelle = db.tbl_Ürünler.Find(veri.ürünID);
            ürünGüncelle.ürünAD = veri.ürünAD;
            ürünGüncelle.ürünFİYAT = veri.ürünFİYAT;
            ürünGüncelle.Marka = veri.Marka;
            ürünGüncelle.Stok = veri.Stok;
            var kategoriVeri = db.tbl_Kategoriler.Where(v => v.kategoriID == veri.tbl_Kategoriler.kategoriID).FirstOrDefault();
            ürünGüncelle.ürünKATEGORİ = kategoriVeri.kategoriID;
            db.SaveChanges();
            return RedirectToAction("Ürünler");
        }
    }
}