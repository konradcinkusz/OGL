using Repozytorium.Models;
using System.Linq;

namespace Repozytorium.IRepo
{
    public interface IOgloszenieRepo
    {
        IQueryable<Ogloszenie> PobierzOgloszenie();
        IQueryable<Ogloszenie> PobierzStrone(int? page, int? pageSize);
        Ogloszenie GetOgloszenieById(int id);
        void UsunOgloszenieById(int id);
        void SaveChanges();
        void Dodaj(Ogloszenie ogloszenie);
        void Aktualizuj(Ogloszenie ogloszenie);
    }
}
