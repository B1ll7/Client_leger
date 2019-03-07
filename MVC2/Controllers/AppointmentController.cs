using MVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC2.Controllers
{
    public class AppointmentController : Controller
    {
        private NoteBookEntities1 db = new NoteBookEntities1();

        public ActionResult SuccessAddAppointment()
        {
            return View();
        }
        // GET: Appointment
        public ActionResult AddAppointment(int? id)
        {
            if (id == null)
            {
                ViewBag.idBroker = new SelectList(db.brokers, "idBroker", "lastName");
                ViewBag.idCustomer = new SelectList(db.customers, "idCustomer", "lastName");
                return View();
            }
            else // Via un ID par la liste
            {
                ViewBag.idBroker = new SelectList(db.brokers, "idBroker", "lastName", id);
                ViewBag.idCustomer = new SelectList(db.customers, "idCustomer", "lastName");
                return View();
            }

        }
        // Instanciation du constructeur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAppointment([Bind(Include = "idAppointment, dateHour, idBroker, idCustomer")] appointment AppointmentToAdd)
        {
            var date = Request.Form["trip-start"];
            var hour = Request.Form["horaire"];
            var datatime = date + " " + hour;
            AppointmentToAdd.dateHour = Convert.ToDateTime(datatime);
            var isAlreadyUsed = db.appointments.Where(appoint => appoint.dateHour == AppointmentToAdd.dateHour && appoint.idBroker == AppointmentToAdd.idBroker || appoint.dateHour == AppointmentToAdd.dateHour && appoint.idCustomer == AppointmentToAdd.idCustomer).SingleOrDefault();

        // je dois recupérer le datetime de la classe addAppointment et les ajouter dans une database de type dateTime ici 

            if(string.IsNullOrEmpty(date) || string.IsNullOrEmpty(hour))
            {
                ModelState.AddModelError("dateHour", "heure ou date manquante");
            }
            else if (isAlreadyUsed != null)
            {
                ModelState.AddModelError("dateHour", "Le courtier ou le client à déjà rendez-vous de prévu à cette date ");
            }
            if (ModelState.IsValid)
            {
                db.appointments.Add(AppointmentToAdd); // insertion dans la dataBase
                db.SaveChanges(); // enregistre les changements
                return RedirectToAction("SuccessAddAppointment");
            }
            ViewBag.idBroker = new SelectList(db.brokers, "idBroker", "lastName", AppointmentToAdd.idBroker);
            ViewBag.idCustomer = new SelectList(db.customers, "idCustomer", "lastName", AppointmentToAdd.idCustomer);
            return View(AppointmentToAdd);
        }

        private class DisplayName
        {
        }
        //new Dictionary<string, string>
        /* */
    }
}