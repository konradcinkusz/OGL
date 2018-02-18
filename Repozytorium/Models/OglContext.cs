using Microsoft.AspNet.Identity.EntityFramework;
using Repozytorium.IRepo;
using Repozytorium.Model.Dictionaries;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Repozytorium.Models
{
    public class OglContext : IdentityDbContext, IOglContext
    {
        public OglContext() : base("DefaultConnection")
        { 

        }
        public static OglContext Create()
        {
            return new OglContext();
        }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Ogloszenie> Ogloszenie  { get; set; }
        public DbSet<Uzytkownik> Uzytkownik  { get; set; }
        public DbSet<Ogloszenie_Kategoria> Ogloszenie_Kategoria  { get; set; }
        public DbSet<Car> Samochody { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Ogloszenie>().HasRequired(x => x.Uzytkownik).WithMany(x => x.Ogloszenia).HasForeignKey(x => x.UzytkownikId).WillCascadeOnDelete(true);
        }
    }
}