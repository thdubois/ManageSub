using ManageSub.App_Start;
using ManageSub.DAL;
using ManageSub.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageSub.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class TicketController : Controller
    {
        private ApplicationUserManager _userManager;
        private ManageSubContext bdd = new ManageSubContext();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Ticket
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ticket/Create
        public async System.Threading.Tasks.Task<ActionResult> Acheter()
        {
            TicketViewModels ticketViewModels = new TicketViewModels();
            ticketViewModels.nbTicket = 0;
            IdentityModels userIdentity = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (userIdentity != null)
            {
                var cartes = userIdentity.CarteModels;
                if (cartes != null && cartes.Count > 0)
                {
                    CarteModels carte = cartes.ElementAt(0);
                    if (carte.TicketModels != null)
                    {
                        ticketViewModels.nbTicket = (uint) carte.TicketModels.Count;
                        if(carte.TicketModels.Count > 0)
                        {
                            TicketModels ticket = carte.TicketModels.First();
                            ticketViewModels.prixTicket = ticket.TarifModels.prix;
                        }
                        else
                        {
                            double prix = bdd.TarifModels.ToList().Find(t => t.type.Contains("ticket")).prix;
                            ticketViewModels.prixTicket = prix;
                        }
                    }
                }
            }
            return View(ticketViewModels);
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Acheter(TicketViewModels ticketvm)
        {
            try
            {
                uint nbTicketSurLaCarte = 0;
                IdentityModels userIdentity = await UserManager.FindByEmailAsync(User.Identity.Name);
                if (userIdentity != null)
                {
                    var cartes = userIdentity.CarteModels;
                    CarteModels carte = cartes.ElementAt(0);
                    nbTicketSurLaCarte = (uint) carte.TicketModels.Count;
                    if (carte != null)
                    {
                        ICollection<TicketModels> ticketList;
                        if (carte.TicketModels == null)
                        {
                            ticketList = new List<TicketModels>();
                        }
                        else
                        {
                            ticketList = carte.TicketModels;
                        }
                        for (int i = 0; i < ticketvm.nbTicket; i++)
                        {
                            TicketModels ticket = new TicketModels();
                            ticket.CarteModelsID = carte.Id;
                            int tarifId = bdd.TarifModels.ToList().Find(t => t.type.Contains("ticket")).Id;
                            ticket.TarifModelsID = tarifId;
                            ticketList.Add(ticket);
                            bdd.TicketModels.Add(ticket);
                        }
                        //carte.TicketModels = ticketList;
                       // bdd.CarteModels.Add(carte);
                        bdd.SaveChanges();
                    }
                }
                ticketvm.nbTicket = (uint) ticketvm.nbTicket + nbTicketSurLaCarte;
                ticketvm.prixTicket = bdd.TarifModels.Find(3).prix;
                //return RedirectToAction("Index");
                return View(ticketvm);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                return View(ticketvm);
            }
        }

        // GET: Ticket/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Ticket/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ticket/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
