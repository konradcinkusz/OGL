using Repozytorium.Models;
using Repozytorium.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repozytorium.IRepo
{
    public interface IKategorie
    {
        IQueryable<Kategoria> PobierzKategorie();
        IQueryable<Ogloszenie> PobierzOgloszeniaZKategorii(int id);
        string NazwaZKategorii(int id);
    }
}
