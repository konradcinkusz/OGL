namespace Repozytorium.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repozytorium.Models.OglContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repozytorium.Models.OglContext context)
        {
            if (!Debugger.IsAttached)
                Debugger.Launch();
            SeedRoles(context);
            SeedUsers(context);
            SeedOgloszenia(context);
            SeedKategorie(context);
            SeedOgloszenie_Kategoria(context);
            SeedUsedNumbers(context);
        }

        private void SeedUsedNumbers(OglContext context)
        {
           for(int i=0;i<100;i++)
                context.Set<UsedNumber>().AddOrUpdate(new UsedNumber { Used = i * 57 });
            context.SaveChanges();
        }

        private void SeedRoles(OglContext context)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            if (!roleManager.RoleExists("Admin"))
            {
                IdentityRole role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Pracownik"))
            {
                IdentityRole role = new IdentityRole { Name = "Pracownik" };
                roleManager.Create(role);
            }
        }
        private void SeedUsers(OglContext context)
        {
            UserStore<Uzytkownik> store = new UserStore<Uzytkownik>(context);
            UserManager<Uzytkownik> manager = new UserManager<Uzytkownik>(store);
            if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                Uzytkownik user = new Uzytkownik { UserName = "Admin", Wiek = 12 };
                IdentityResult adminResult = manager.Create(user, "123456789");
                if (adminResult.Succeeded)
                    manager.AddToRole(user.Id, "Admin");
            }
            if (!context.Users.Any(u => u.UserName == "Marek"))
            {
                Uzytkownik user = new Uzytkownik { UserName = "marek@AspNetMvc.pl", Wiek = 48 };
                IdentityResult adminResult = manager.Create(user, "123456789");
                if (adminResult.Succeeded)
                    manager.AddToRole(user.Id, "Pracownik");
            }
            if (!context.Users.Any(u => u.UserName == "Prezes"))
            {
                Uzytkownik user = new Uzytkownik { UserName = "prezes@AspNetMvc.pl", Wiek = 33 };
                IdentityResult adminResult = manager.Create(user, "123456789");
                if (adminResult.Succeeded)
                    manager.AddToRole(user.Id, "Admin");
            }
        }
        private void SeedOgloszenia(OglContext context)
        {
            string idUzytkownika = context.Set<Uzytkownik>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;
            for (int i = 1; i <= 10; i++)
            {
                Ogloszenie ogl = new Ogloszenie()
                {
                    ID = i,
                    UzytkownikId = idUzytkownika,
                    Tresc = $"Treść ogłoszenia: {i}",
                    Tytul = $"Tytul ogłoszenia: {i}",
                    DataDodania = DateTime.Now.AddDays(-i)
                };
                context.Set<Ogloszenie>().AddOrUpdate(ogl);
            }
            context.SaveChanges();
        }
        private void SeedKategorie(OglContext context)
        {
            for (int i = 1; i <= 10; i++)
            {
                Kategoria kat = new Kategoria()
                {
                    Id = i,
                    Nazwa = $"Nazwa kategorii {i}",
                    Tresc = $"Tresc kategorii {i}",
                    MetaTytul = $"Metatutl kategorii {i}",
                    MetaOpis = $"metaopis kategorii {i}",
                    MetaSlowa = $"Słowa kluczowe do kategorii {i}",
                    ParentId = i
                };
                context.Set<Kategoria>().AddOrUpdate(kat);
            }
            context.SaveChanges();
        }
        private void SeedOgloszenie_Kategoria(OglContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                Ogloszenie_Kategoria okat = new Ogloszenie_Kategoria()
                {
                    Id = i,
                    OgloszenieId = i /2 + 1,
                    KategoriaId = i / 2 + 2
                };

                context.Set<Ogloszenie_Kategoria>().AddOrUpdate(okat);
            }
            context.SaveChanges();
        }
    }
}
