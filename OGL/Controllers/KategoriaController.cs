using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repozytorium.Models;
using Repozytorium.IRepo;
using Repozytorium.Models.View;

namespace OGL.Controllers
{
    public class KategoriaController : Controller
    {
        private readonly IKategorie _repo;
        public KategoriaController(IKategorie repo)
        {
            _repo = repo;
        }
        // GET: Kategoria
        public ActionResult Index()
        {
            var kategorie = _repo.PobierzKategorie();
            return View(kategorie);
        }

        public ActionResult PokazOgloszenia(int id)
        {
            var ogloszenia = _repo.PobierzOgloszeniaZKategorii(id);
            OgloszenieZKategoriiViewModels model = new OgloszenieZKategoriiViewModels();
            model.Ogloszenia = ogloszenia.ToList();
            model.NazwaKategorii = _repo.NazwaZKategorii(id);
            return View(model);
        }
        [Route("JSON")]
        public ActionResult KategorieWJSON()
        {
            var kategorie = _repo.PobierzKategorie();
            return Json(kategorie, JsonRequestBehavior.AllowGet);
        }
        
    }
}
