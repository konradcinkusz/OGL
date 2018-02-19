using Repozytorium.Logic;
using Repozytorium.Model.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace OGL.IoC
{
    public class Bootstrap
    {
        public static UnityContainer Register()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IRepository<Car>, Repository<Car>>();
            return container;
        }
    }
}
