using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Covoiturage;
using Covoiturage.Models;
using Microsoft.AspNet.Identity;

namespace Covoiturage.Controllers
{
    public class TrajetsController : Controller
    {
        private CovoiturageDBEntities db = new CovoiturageDBEntities();

        // GET: Trajets
        public ActionResult Index()
        {
            var trajet = db.Trajet.Include(t => t.AspNetUsers);
            return View(trajet.ToList());
        }

        // GET: Trajets/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trajet trajet = db.Trajet.Find(id);
            if (trajet == null)
            {
                return HttpNotFound();
            }
            return View(trajet);
        }

        // GET: Trajets/Create
        public ActionResult ProposerTrajet()
        {
            return View();
        }

        // POST: Trajets/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lieu_Depart,Lieu_Arrivee,Date_Depart,Heure_Depart,Nbr_Places_Dispo")] Trajet trajet)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    string Id_Client = User.Identity.GetUserId();
                    trajet.Id = Id_Client;
                    trajet.Id_Trajet = Id_Client + "-" + DateTime.Now.ToString("dd-MM-yyyy");
                    db.Trajet.Add(trajet);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return View(trajet);
        }

        // GET: Trajets/RechercherTrajet
        public ActionResult RechercherTrajet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RechercheTrajet(RechercherTrajetViewModel vm)
        {
            return View(db.Trajet.Where(t => t.Lieu_Depart == vm.Lieu_Depart && t.Lieu_Arrivee == vm.Lieu_Arrivee && t.Date_Depart.ToString().Contains(vm.Date_Depart.ToString())).ToList());
        }

        public ActionResult getTrajet()
        {
            var trajet = db.Trajet.ToList();
            return Json(trajet);
        }

        // GET: Trajets/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trajet trajet = db.Trajet.Find(id);
            if (trajet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", trajet.Id);
            return View(trajet);
        }

        // POST: Trajets/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Trajet,Lieu_Depart,Lieu_Arrivee,Date_Depart,Heure_Depart,Nbr_Places_Dispo,Id")] Trajet trajet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trajet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", trajet.Id);
            return View(trajet);
        }

        // GET: Trajets/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trajet trajet = db.Trajet.Find(id);
            if (trajet == null)
            {
                return HttpNotFound();
            }
            return View(trajet);
        }

        // POST: Trajets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Trajet trajet = db.Trajet.Find(id);
            db.Trajet.Remove(trajet);
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
