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
        public ActionResult AddAppointment()
        {
            ViewBag.idBroker = new SelectList(db.brokers, "idBroker", "lastName");
            ViewBag.idCustomer = new SelectList(db.customers, "idCustomer", "lastName");
            return View();
        }
        // Instanciation du constructeur
        [HttpPost]
        public ActionResult AddAppointment([Bind(Include = "idBroker, idCustomer, idAppointment, dateHour")] appointment AppointmentToAdd)
        {
            var date = Request.Form["trip-start"];
            var hour = Request.Form["horaire"];
            var datatime = date + " " + hour;// cet etape manque
            AppointmentToAdd.dateHour = Convert.ToDateTime(datatime);
            // je dois recupérer le datetime de la classe addAppointment et les ajouter dans une database de type dateTime ici 
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