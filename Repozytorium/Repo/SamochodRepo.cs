using Repozytorium.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repozytorium.Repo
{
    public class SamochodRepo
    {
        private readonly IOglContext _db;
        public SamochodRepo(IOglContext db)
        {
            _db = db;
        }

    }
}