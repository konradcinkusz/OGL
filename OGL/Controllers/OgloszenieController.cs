using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repozytorium.Models;
using System.Diagnostics;
using Repozytorium.Repo;
using Repozytorium.IRepo;
using Microsoft.AspNet.Identity;
using PagedList;

namespace OGL.Controllers
{
    public class OgloszenieController : Controller
    {
        private readonly IOgloszenieRepo _repo;

        public OgloszenieController(IOgloszenieRepo repo)
        {
            _repo = repo;
        }
        [OutputCache(Duration =1000)]
        public ActionResult MojeOgloszenia(int? page)
        {
            var ogloszenie = _repo.PobierzOgloszenie();
            var userId = User.Identity.GetUserId();
            ogloszenie = ogloszenie.OrderByDescending(y=>y.DataDodania).Where(x => x.UzytkownikId == userId);
            int currentIndex = page ?? 1;
            int elementOnPage = 5;
            //Jak nie będzie orderowania to walnie błędem:
            //Additional information: The method 'Skip' is only supported for sorted input in LINQ to Entities.The method 'OrderBy' must be called before the method 'Skip'.
            return View(ogloszenie.ToPagedList<Ogloszenie>(currentIndex, elementOnPage));

        }
        // GET: Ogloszenie
        public ActionResult Index(int? page, string sortOrder)
        {
            var ogloszenie = _repo.PobierzOgloszenie();
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "IdAsc" : "";
            ViewBag.TrescSort = sortOrder == "Tresc" ? "TrescAsc" : "Tresc";
            ViewBag.DataDodaniaSort = sortOrder == "DataDodania" ? "DataDodaniaAsc" : "DataDodania";
            ViewBag.TytulSort = sortOrder == "Tytul" ? "TytulAsc" : "Tytul";
            switch (sortOrder)
            {
                case "TrescAsc":
                    ogloszenie = ogloszenie.OrderBy(x => x.Tresc);
                    break;
                case "Tresc":
                    ogloszenie = ogloszenie.OrderByDescending(x => x.Tresc);
                    break;
                case "DataDodania":
                    ogloszenie = ogloszenie.OrderByDescending(x => x.DataDodania);
                    break;
                case "DataDodaniaAsc":
                    ogloszenie = ogloszenie.OrderBy(x => x.DataDodania);
                    break;
                case "Tytul":
                    ogloszenie = ogloszenie.OrderByDescending(x => x.Tytul);
                    break;
                case "TytulAsc":
                    ogloszenie = ogloszenie.OrderBy(x => x.Tytul);
                    break;
                case "IdAsc":
                    ogloszenie = ogloszenie.OrderBy(x => x.ID);
                    break;
                default:
                    ogloszenie = ogloszenie.OrderByDescending(x => x.ID);
                    break;
            }
            int currentIndex = page ?? 1;
            int elementOnPage = 5;
            //Jak nie będzie orderowania to walnie błędem:
            //Additional information: The method 'Skip' is only supported for sorted input in LINQ to Entities.The method 'OrderBy' must be called before the method 'Skip'.
            return View(ogloszenie.ToPagedList<Ogloszenie>(currentIndex, elementOnPage));
        }

        public ActionResult Partial()
        {
            var ogloszenie = _repo.PobierzOgloszenie();
            return PartialView("Index", ogloszenie);//w poprzednich akcjach nazwa była taka sama jak z plik z widokiem, więc nie trzeba było podawać pierwszego parametru
        }
        //// GET: Ogloszenie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = _repo.GetOgloszenieById(id.Value);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            return View(ogloszenie);
        }


        // GET: Ogloszenie/Create
        [Authorize] //Przekierowanie nastąpi przed otwarciem strony
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ogloszenie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]//Przekierowanie nastąpi po otwarciu strony
        public ActionResult Create([Bind(Include = "Tresc,Tytul")] Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                ogloszenie.UzytkownikId = User.Identity.GetUserId();
                ogloszenie.DataDodania = DateTime.Now;
                _repo.Dodaj(ogloszenie);
                try
                {
                    _repo.SaveChanges();
                    return RedirectToAction("MojeOgloszenia");
                }
                catch (Exception ex)
                {
                    return View(ogloszenie);
                }
            }

            return View(ogloszenie);
        }

        // GET: Ogloszenie/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = _repo.GetOgloszenieById(id.Value);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            else if (ogloszenie.UzytkownikId == User.Identity.GetUserId() && User.IsInRole("Admin") || User.IsInRole("Pracownik"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(ogloszenie);
        }

        // POST: Ogloszenie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken, Authorize]
        public ActionResult Edit([Bind(Include = "ID,Tresc,Tytul,DataDodania,UzytkownikId")] Ogloszenie ogloszenie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ogloszenie.UzytkownikId = "asfas";
                    _repo.Aktualizuj(ogloszenie);
                    _repo.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Blad = true;
                    return View(ogloszenie);
                }
            }
            ViewBag.Blad = false;
            return View(ogloszenie);
        }

        // GET: Ogloszenie/Delete/5
        [Authorize]
        public ActionResult Delete(int? id, bool? blad)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogloszenie ogloszenie = _repo.GetOgloszenieById(id.Value);
            if (ogloszenie == null)
            {
                return HttpNotFound();
            }
            else if (ogloszenie.UzytkownikId == User.Identity.GetUserId() && User.IsInRole("Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (blad != null)
                ViewBag.Blad = true;

            return View(ogloszenie);
        }

        //POST: Ogloszenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repo.UsunOgloszenieById(id);
            try
            {
                _repo.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { id = id, blad = true });
            }
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
