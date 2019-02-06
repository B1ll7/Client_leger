using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions; //Pour utiliser les regex
using MVC2.Models;

namespace MVC2.Controllers
{
    public class BrokerController : Controller
    {
        private NoteBookEntities1 db = new NoteBookEntities1();


        // GET: ProfilBroker
        public ActionResult ProfilBroker()
        {
            return View();
        }

        // GET: ListBroker
        public ActionResult ListBroker()
        {
            return View(db.brokers.ToList());
        }
        // GET: Broker
        public ActionResult AddBroker()
        {
            return View("AddBroker");
        }
        // Instanciation du constructeur
        [HttpPost]
        public ActionResult AddBroker([Bind(Include ="idBroker, lastName, firstName, mail, phoneNumber")] broker BrokerToAdd)
        {
            string regexName = @"^[A-Za-zéèëêuùçôàö\-]+$";
            string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            string regexPhone = @"^[0][0-9]{9}";
            if (!string.IsNullOrEmpty(BrokerToAdd.lastName))
            {
                // verification de regex
                if(!Regex.IsMatch(BrokerToAdd.lastName, regexName))
                {
                    ModelState.AddModelError("LastName", "Ecrire un nom valide");
                }
            }
            // Verification si le champs est libre ou pas
            else
            {
                ModelState.AddModelError("LastName", "Ecrire un nom");
            }
            if (!string.IsNullOrEmpty(BrokerToAdd.firstName))
            {
                if(!Regex.IsMatch(BrokerToAdd.firstName, regexName))
                {
                    ModelState.AddModelError("firstName", "Ecrire un prénom valide");
                }
            }
            else
            {
                ModelState.AddModelError("firstName", "Ecrire un prénom");
            }
            if (!string.IsNullOrEmpty(BrokerToAdd.mail))
            {
                if(!Regex.IsMatch(BrokerToAdd.mail, regexMail))
                {
                    ModelState.AddModelError("mail", "Ecrire une adresse mail valide");
                }
            }
            else
            {
                ModelState.AddModelError("mail", "Ecrire une adresse mail");
            }
            if (!string.IsNullOrEmpty(BrokerToAdd.phoneNumber))
            {
                if(!Regex.IsMatch(BrokerToAdd.phoneNumber, regexPhone))
                {
                    ModelState.AddModelError("phoneNumber", "Ecrire une numero de tel valide");
                }
            }
            else
            {
                ModelState.AddModelError("phoneNumber", "Ecrire une numero de tel");
            }
            if(ModelState.IsValid)
            {
                db.brokers.Add(BrokerToAdd); // insertion dans la dataBase
                db.SaveChanges(); // enregistre les changements
                return RedirectToAction("SuccessAddBroker");
            }
            else
            {
                return View(BrokerToAdd); // Il y'a des erreurs
            }
        }
        public ActionResult SuccessAddBroker()
        {
            return View("SuccessAddBroker");
        }
    }
}