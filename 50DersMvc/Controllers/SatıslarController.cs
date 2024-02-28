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
    public class SatıslarController : Controller
    {
        // GET: Satıslar
        Entities DbContext = new Entities();
        
        [HttpGet]
        public ActionResult Satıslar(int sayfa = 1)
        {
            List<SelectListItem> degerler = (from i in DbContext.tbl_Ürünler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.ürünAD,
                                                 Value = i.ürünID.ToString()
                                             }).ToList();
            List<SelectListItem> degerler1 = (from i in DbContext.tbl_Müsteriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.müsteriAD + "" + i.müsteriSOYAD,
                                                 Value = i.müsteriID.ToString()
                                             }).ToList();
            ViewBag.veri = degerler;
            ViewBag.veri1 = degerler1;
           

            IPagedList<tbl_Satislar> SatıslarListesi1 = DbContext.tbl_Satislar.ToList().ToPagedList(sayfa,4);
            ViewBag.SatıslarListesi = SatıslarListesi1;

            

            return View();
        }
        public ActionResult SatıslarAra(string p)
        {
            var degerler = from i in DbContext.tbl_Satislar select i;

            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(v => v.tbl_Ürünler.ürünAD.Contains(p));
            }
            return View(degerler.ToList());
        }
        [HttpPost]
        public ActionResult YeniSatıs(tbl_Satislar veri)
        {
            var ÜrünVeri = DbContext.tbl_Ürünler.Where(v => v.ürünID == veri.tbl_Ürünler.ürünID).FirstOrDefault();
            veri.tbl_Ürünler = ÜrünVeri;

            var MüsteriVeri = DbContext.tbl_Müsteriler.FirstOrDefault(v => v.müsteriID == veri.tbl_Müsteriler.müsteriID);
            veri.tbl_Müsteriler = MüsteriVeri;

            DbContext.tbl_Satislar.Add(veri);
            DbContext.SaveChanges();
            return RedirectToAction("Satıslar");
        }
       

    }
}