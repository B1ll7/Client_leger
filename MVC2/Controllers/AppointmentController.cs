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
        // GET: Appointment
        public ActionResult AddAppointment()
        {
            ViewBag.idBroker = new SelectList(db.brokers, "idBroker", "lastName", "firstName");
            ViewBag.idCustomer = new SelectList(db.customers, "idCustomer", "lastName", "firstName");
            ViewBag.idAppointment = new SelectList(db.appointments, "idAppoitment", "dateHour");
            return View();
        }

        /* */
    }
}