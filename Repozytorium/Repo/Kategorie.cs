using Repozytorium.IRepo;
using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repozytorium.Repo
{
    public class Kategorie : IKategorie
    {
        private readonly IOglContext _db;
        public Kategorie(IOglContext db)
        {
            _db = db;
        }
        public IQueryable<Kategoria> PobierzKategorie()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var kategoria = _db.Kategorie.AsNoTracking();
            return kategoria;
        }
        public IQueryable<Ogloszenie> PobierzOgloszeniaZKategorii(int id)
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var ogloszenia =
                from o in _db.Ogloszenie
                join k in _db.Ogloszenie_Kategoria on o.ID equals k.Id
                where k.KategoriaId == id
                select o;
            return ogloszenia;
        }
        public string NazwaZKategorii(int id)
        {
            return _db.Kategorie.Find(id).Nazwa;
        }
    }
}