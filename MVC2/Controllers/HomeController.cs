using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC2.Models;
using System.Data.Entity;
using System.Net;

namespace MVC2.Controllers
{
    public class HomeController : Controller
    {
        private NoteBookEntities1 db = new NoteBookEntities1();
        // GET: Home
        public ActionResult Index()
        {
            var appointments = db.appointments.Include(a => a.broker).Include(a => a.customer);
            return View(appointments.ToList());
        }
        // GET: More page
        public ActionResult More(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment appointments = db.appointments.Find(id);
            if (appointments == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        public ActionResult DeleteAppointment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            appointment appointments = db.appointments.Find(id);
            if (appointments == null)
            {
                return HttpNotFound();
            }
            return View(appointments);
        }
        [HttpPost, ActionName("DeleteAppointment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            appointment appointments = db.appointments.Find(id);
            db.appointments.Remove(appointments);
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
        public ActionResult ProfilAppointment(int? id)
        {
            appointment appointmentById = db.appointments.Find(id);
            return View(appointmentById);
        }
    }
}