using Repozytorium.Logic;
using Repozytorium.Model.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace OGL.Services
{
    public class CarService
    {
        IRepository<Car> _repo;
        public CarService(IRepository<Car> repo)
        {
            _repo = repo;
        }
        public void Add(Car obj) => _repo.Add(obj);
        public Car FindById(int id) => _repo.FindBy(x => x.ID == id).ToList().First();
        public void Remove(Car obj) => _repo.Remove(obj);
        public void Save() => _repo.Save();
    }
}
