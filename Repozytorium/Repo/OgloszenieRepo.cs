using Repozytorium.IRepo;
using Repozytorium.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Repozytorium.Repo
{
    public class OgloszenieRepo : IOgloszenieRepo
    {
        private readonly IOglContext _db;
        public OgloszenieRepo(IOglContext db)
        {
            _db = db;
        }
        public IQueryable<Ogloszenie> PobierzOgloszenie()
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            return _db.Ogloszenie;
        }
        public IQueryable<Ogloszenie> PobierzStrone(int? page = 1, int? pageSize = 15)
        {
            return _db.Ogloszenie.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }
        public Ogloszenie GetOgloszenieById(int id)
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var ogloszenie = _db.Ogloszenie.Find(id);
            return ogloszenie;// _db.Ogloszenie.AsNoTracking().Single(x=>x.ID == id);
        }
        private void UsunPowiwazanie(int id)
        {
            var lista = _db.Ogloszenie_Kategoria.Where(x => x.Id == id);
            foreach (var el in lista)
            {
                _db.Ogloszenie_Kategoria.Remove(el);
            }
        }
        public void UsunOgloszenieById(int id)
        {
            _db.Database.Log = message => Trace.WriteLine(message);
            var ogloszenie = _db.Ogloszenie.Find(id);
            _db.Ogloszenie.Remove(ogloszenie);
        }
        public void SaveChanges()
        {
            _db.SaveChanges();
        }
        public void Dodaj(Ogloszenie ogloszenie)
        {
            _db.Ogloszenie.Add(ogloszenie);
        }
        public void Aktualizuj(Ogloszenie ogloszenie)
        {
            _db.Entry(ogloszenie).State = EntityState.Modified;
        }
    }
}