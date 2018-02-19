using OGL.IoC;
using OGL.Services;
using Repozytorium.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            NumberGenerator g = new NumberGenerator();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
                g.Generate();
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }
}
