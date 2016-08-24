using ManageSub.App_Start;
using ManageSub.DAL;
using ManageSub.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageSub.Controllers
{
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
        public ActionResult Create()
        {
            TicketViewModels ticketViewModels = new TicketViewModels
            {
                nbTicket = 0
        };
            return View(ticketViewModels);
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Create(TicketViewModels ticketvm)
        {
            try
            {
                IdentityModels userIdentity = await UserManager.FindByEmailAsync(User.Identity.Name);
                if (userIdentity != null)
                {
                    var cartes = userIdentity.CarteModels;
                    CarteModels carte = cartes.ElementAt(0);
                    if (carte != null)
                    {
                        List<TicketModels> ticketList = new List<TicketModels>();
                        for(int i=0; i< ticketvm.nbTicket; i++)
                        {
                            TicketModels ticket = new TicketModels();
                            ticket.CarteModels = carte;
                            ticketList.Add(ticket);
                            bdd.TicketModels.Add(ticket);
                        }
                        carte.TicketModels = ticketList;
                        bdd.CarteModels.Add(carte);
                        bdd.SaveChanges();
                    }
                }
                //return RedirectToAction("Index");
                return View(ticketvm);
            }
            catch
            {
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
