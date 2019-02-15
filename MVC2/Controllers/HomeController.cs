using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC2.Models;
using System.Data.Entity;

namespace MVC2.Controllers
{
    public class HomeController : Controller
    {
        private NoteBookEntities1 db = new NoteBookEntities1();
        // GET: Home
        public ActionResult Index()
        {
            var listAppointment = db.appointments.SqlQuery("SELECT [dbo].[brokers].[lastName], [dbo].[customers].[lastName], [datehour] " +
                "FROM [dbo].[appointment] " +
                "LEFT JOIN [dbo].[broker] ON [broker].[id] = [appointment].[idBroker] " +
                "LEFT JOIN [dbo].[customer] ON [customer].[id] = [appointment].[idCustomer]");
            //var appointments = db.appointments.Include(a => a.broker).Include(a => a.customer);
            return View(listAppointment);
        }
    }
}