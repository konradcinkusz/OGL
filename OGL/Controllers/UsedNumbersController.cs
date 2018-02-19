using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Repozytorium.Model;
using Repozytorium.Models;

namespace OGL.Controllers
{
    public class UsedNumbersController : Controller
    {
        private OglContext db = new OglContext();

        // GET: UsedNumbers
        public ActionResult Index()
        {
            return View(db.NumeryWykorzystane.ToList());
        }

        // GET: UsedNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedNumber usedNumber = db.NumeryWykorzystane.Find(id);
            if (usedNumber == null)
            {
                return HttpNotFound();
            }
            return View(usedNumber);
        }

        // GET: UsedNumbers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsedNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Used")] UsedNumber usedNumber)
        {
            if (ModelState.IsValid)
            {
                db.NumeryWykorzystane.Add(usedNumber);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usedNumber);
        }

        // GET: UsedNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedNumber usedNumber = db.NumeryWykorzystane.Find(id);
            if (usedNumber == null)
            {
                return HttpNotFound();
            }
            return View(usedNumber);
        }

        // POST: UsedNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Used")] UsedNumber usedNumber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usedNumber).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usedNumber);
        }

        // GET: UsedNumbers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsedNumber usedNumber = db.NumeryWykorzystane.Find(id);
            if (usedNumber == null)
            {
                return HttpNotFound();
            }
            return View(usedNumber);
        }

        // POST: UsedNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsedNumber usedNumber = db.NumeryWykorzystane.Find(id);
            db.NumeryWykorzystane.Remove(usedNumber);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
