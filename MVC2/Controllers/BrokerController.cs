using System;
using System.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions; // Pour l'utilisation des Regex
using MVC2.Models;
using System.Data.Entity;
using System.Net;

namespace MVC2.Controllers
{
    public class BrokerController : Controller
    {
        private NoteBookEntities1 db = new NoteBookEntities1();

        // GET: Une methode qui permet d'afficher le ProfilBroker
        public ActionResult ProfilBroker(int? id) // '?' rendre nullable l'id
        { 
            if (id == null)
            {
                return View("ErrorBadRequest");
            }
            // recupère le broker par son id. 
            broker brokerById = db.brokers.Find(id);
            if (brokerById == null)
            {
                return View("ErrorPageNotFound");
            }
            return View(brokerById);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ProfilBroker([Bind(Include = "idBroker,lastName,firstName,mail,phoneNumber")] broker brokerList)
        {
            //Déclaration des Regexs
            string regexName = @"^[A-Za-zéèàêâôûùïüç-]+$";
            string regexMail = @"^[0-9a-zA-Z.-]+@[0-9a-zA-Z.-]+.[a-zA-Z]{2,4}";
            string regexPhone = @"^[0][0-9]{9}";
            //Vérification que le champ lastName ne soit pas incorrect ou null
            if (!String.IsNullOrEmpty(brokerList.lastName))  //si le champ mail n'est pas vide ou null on vérifie la validité de l'entrée
            {
                if (!Regex.IsMatch(brokerList.lastName, regexName))
                {
                    //Message d'erreur
                    ModelState.AddModelError("lastName", "Votre nom n'est pas valide");
                }
            }
            else
            {
                //Message d'erreur si le lastName est vide ou null
                ModelState.AddModelError("lastName", "Veuillez remplir ce champ");
            }
            //Vérification que le champ firstName ne soit pas incorrect ou null
            if (!String.IsNullOrEmpty(brokerList.firstName)) //si le champ firstName n'est pas vide ou null on vérifie la validité de l'entrée
            {
                if (!Regex.IsMatch(brokerList.firstName, regexName)) //si l'entrée utilisateur ne passe pas la regex ajout d'un message d'erreur
                {
                    //Message d'erreur
                    ModelState.AddModelError("firstName", "Votre prénom n'est pas valide");
                }
            }
            else
            {
                //Message d'erreur si le firstName est vide ou null
                ModelState.AddModelError("firstName", "Veuillez remplir ce champ");
            }
            //Vérification que le champ email ne soit pas incorrect ou null
            if (!String.IsNullOrEmpty(brokerList.mail)) //si le champ mail n'est pas vide ou null on vérifie la validité de l'entrée
            {
                if (!Regex.IsMatch(brokerList.mail, regexMail))
                {
                    //Message d'erreur
                    ModelState.AddModelError("mail", "Veuillez écrire un email valide");
                }
            }
            else
            {
                //Message d'erreur si le mail est vide ou null
                ModelState.AddModelError("mail", "Veuillez remplir ce champ");
            }
            if (!String.IsNullOrEmpty(brokerList.phoneNumber)) //si le champ phoneNumber n'est pas vide ou null on vérifie la validité de l'entrée
            {
                if (!Regex.IsMatch(brokerList.phoneNumber, regexPhone))
                {
                    //Message d'erreur
                    ModelState.AddModelError("phoneNumber", "Veuillez saisir un numéro valide");
                }
            }
            else
            {
                //Message d'erreur si le numéro de téléphone est vide ou null
                ModelState.AddModelError("phoneNumber", "Veuillez remplir ce champ");
            }
            if (ModelState.IsValid)
            {
                db.Entry(brokerList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("successAddBroker");
            }
            else
            {
                return View("ProfilBroker");
            }
        }
        //GET: ListBroker
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
        public ActionResult AddBroker([Bind(Include = "idBroker, lastName, firstName, mail, phoneNumber")] broker BrokerToAdd)
        {
            string regexName = @"^[A-Za-zéèëêuùçôàö\-]+$";
            string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            string regexPhone = @"^[0][0-9]{9}";
            if (!string.IsNullOrEmpty(BrokerToAdd.lastName))
            {
                // verification de regex
                if (!Regex.IsMatch(BrokerToAdd.lastName, regexName))
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
                if (!Regex.IsMatch(BrokerToAdd.firstName, regexName))
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
                if (!Regex.IsMatch(BrokerToAdd.mail, regexMail))
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
                if (!Regex.IsMatch(BrokerToAdd.phoneNumber, regexPhone))
                {
                    ModelState.AddModelError("phoneNumber", "Ecrire une numero de tel valide");
                }
            }
            else
            {
                ModelState.AddModelError("phoneNumber", "Ecrire une numero de tel");
            }
            if (ModelState.IsValid)
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