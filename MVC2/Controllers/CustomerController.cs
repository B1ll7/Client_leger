using System;
using System.Linq;
using System.Web.Mvc;
using System.Text.RegularExpressions; // Pour l'utilisation des Regex
using MVC2.Models;
using System.Data.Entity;
using System.Net;

namespace MVC2.Controllers
{   
    public class CustomerController : Controller
    {
        private NoteBookEntities1 db = new NoteBookEntities1();

        //GET: ListCustomer
        public ActionResult ListCustomer()
        {
            return View(db.customers.ToList());
        }
        // GET: Customer
        public ActionResult AddCustomer()
        {
            return View("AddCustomer");
        }
        // DELETE: Customer
        public ActionResult DeleteCustomer()
        {
            return View("DeleteCustomer");
        }
        //redirection vers la page de succès
        public ActionResult SuccessAddCustomer()
        {
            return View("SuccessAddCustomer");
        }
        // Instanciation du constructeur
        [HttpPost]
        public ActionResult AddCustomer([Bind(Include = "idCustomer, lastName, firstName, mail, phoneNumber, budget, subject")] customer CustomerToAdd)
        {
            string regexName = @"^[A-Za-zéèëêuùçôàö\-]+$";
            string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            string regexPhone = @"^[0][0-9]{9}";
            string regexSubject = @"^[A-Za-zéèëêuùçôàö\-' ]+$";
            if (!string.IsNullOrEmpty(CustomerToAdd.lastName))
            {
                // verification de regex
                if (!Regex.IsMatch(CustomerToAdd.lastName, regexName))
                {
                    ModelState.AddModelError("LastName", "Ecrire un nom valide");
                }
            }
            // Verification si le champs est libre ou pas
            else
            {
                ModelState.AddModelError("LastName", "Ecrire un nom");
            }
            if (!string.IsNullOrEmpty(CustomerToAdd.firstName))
            {
                if (!Regex.IsMatch(CustomerToAdd.firstName, regexName))
                {
                    ModelState.AddModelError("firstName", "Ecrire un prénom valide");
                }
            }
            else
            {
                ModelState.AddModelError("firstName", "Ecrire un prénom");
            }
            if (!string.IsNullOrEmpty(CustomerToAdd.mail))
            {
                if (!Regex.IsMatch(CustomerToAdd.mail, regexMail))
                {
                    ModelState.AddModelError("mail", "Ecrire une adresse mail valide");
                }
            }
            else
            {
                ModelState.AddModelError("mail", "Ecrire une adresse mail");
            }
            if (!string.IsNullOrEmpty(CustomerToAdd.phoneNumber))
            {
                if (!Regex.IsMatch(CustomerToAdd.phoneNumber, regexPhone))
                {
                    ModelState.AddModelError("phoneNumber", "Ecrire une numero de tel valide");
                }
            }
            else
            {
                ModelState.AddModelError("phoneNumber", "Ecrire une numero de tel");
            }
            if (!string.IsNullOrEmpty(CustomerToAdd.subject))
            {
                if (!Regex.IsMatch(CustomerToAdd.subject, regexSubject))
                {
                    ModelState.AddModelError("subject", "Erreur dans l'écriture du sujet réécrivez s'il vous plait");
                }
            }
            else
            {
                ModelState.AddModelError("subject", "Ecrire un sujet");
            }
            if (ModelState.IsValid)
            {
                db.customers.Add(CustomerToAdd); // insertion dans la dataBase
                db.SaveChanges(); // enregistre les changements
                return RedirectToAction("SuccessAddCustomer");
            }
            else
            {
                return View(CustomerToAdd); // Il y'a des erreurs
            }
        }
        // GET: ProfilCustomer
        public ActionResult ProfilCustomer(int? id)
        {
            if (id == null)
            {
                return View("ErrorBadRequest");
            }
            // recupère le broker par son id. 
            customer customerById = db.customers.Find(id);
            if (customerById == null)
            {
                return View("ErrorPageNotFound");
            }
            return View(customerById);
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ProfilCustomer([Bind(Include = "idCustomer,lastName,firstName,mail,phoneNumber,budget,subject")] customer CustomerList)
        {
            string regexName = @"^[A-Za-zéèëêuùçôàö\-]+$";
            string regexMail = @"[0-9a-zA-Z\.\-]+@[0-9a-zA-Z\.\-]+.[a-zA-Z]{2,4}";
            string regexPhone = @"^[0][0-9]{9}";
            string regexSubject = @"^[A-Za-zéèëêuùçôàö\-' ]+$";
            if (!string.IsNullOrEmpty(CustomerList.lastName))
            {
                // verification de regex
                if (!Regex.IsMatch(CustomerList.lastName, regexName))
                {
                    ModelState.AddModelError("LastName", "Ecrire un nom valide");
                }
            }
            // Verification si le champs est libre ou pas
            else
            {
                ModelState.AddModelError("LastName", "Ecrire un nom");
            }
            if (!string.IsNullOrEmpty(CustomerList.firstName))
            {
                if (!Regex.IsMatch(CustomerList.firstName, regexName))
                {
                    ModelState.AddModelError("firstName", "Ecrire un prénom valide");
                }
            }
            else
            {
                ModelState.AddModelError("firstName", "Ecrire un prénom");
            }
            if (!string.IsNullOrEmpty(CustomerList.mail))
            {
                if (!Regex.IsMatch(CustomerList.mail, regexMail))
                {
                    ModelState.AddModelError("mail", "Ecrire une adresse mail valide");
                }
            }
            else
            {
                ModelState.AddModelError("mail", "Ecrire une adresse mail");
            }
            if (!string.IsNullOrEmpty(CustomerList.phoneNumber))
            {
                if (!Regex.IsMatch(CustomerList.phoneNumber, regexPhone))
                {
                    ModelState.AddModelError("phoneNumber", "Ecrire une numero de tel valide");
                }
            }
            else
            {
                ModelState.AddModelError("phoneNumber", "Ecrire une numero de tel");
            }
            if (!string.IsNullOrEmpty(CustomerList.subject))
            {
                if (!Regex.IsMatch(CustomerList.subject, regexSubject))
                {
                    ModelState.AddModelError("subject", "Erreur dans l'écriture du sujet réécrivez s'il vous plait");
                }
            }
            else
            {
                ModelState.AddModelError("subject", "Ecrire un sujet");
            }
            if (ModelState.IsValid)
            {
                db.Entry(CustomerList).State = EntityState.Modified; // insertion dans la dataBase
                db.SaveChanges(); // enregistre les changements
                return RedirectToAction("SuccessAddCustomer");
            }
            else
            {
                return View(CustomerList); // Il y'a des erreurs
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customers = db.customers.Find(id);
            if (customers == null)
            {
                return HttpNotFound();
            }
            return View(customers);
        }
        // POST: customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer customers = db.customers.Find(id);
            db.customers.Remove(customers);
            db.SaveChanges();
            return RedirectToAction("listCustomer");
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
}