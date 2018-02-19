using OGL.IoC;
using OGL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repozytorium.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Unity.UnityContainer con = Bootstrap.Register();
        }
    }
}
