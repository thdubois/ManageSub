using ManageSub.App_Start;
using ManageSub.DAL;
using ManageSub.Interface;
using ManageSub.Models;
using ManageSub.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageSub.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class AbonnementController : Controller
    {
        public AbonnementController()
        {
            this.carte = new CarteRepository(bdd, UserManager);
        }

        private ICarteRepository carte;
        private ManageSubContext bdd = new ManageSubContext();

        private ApplicationUserManager _userManager;
        public UserManager<IdentityModels> UserManager
        {
            get
            {
                if (_userManager != null)
                {
                    return _userManager;
                }
                else
                {
                    var userStore = new UserStore<IdentityModels>(bdd);
                    var userManager = new UserManager<IdentityModels>(userStore);
                    return userManager;
                }
            }
        }
        // GET: Abonnement
        public ActionResult Index()
        {
            return View();
        }

        // GET: Abonnement/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Abonnement/Create
        public ActionResult Souscrire()
        {
            AbonnementViewModels aboViewModels = new AbonnementViewModels();
            var allTypeAbonnement = bdd.TypeAbonnementModels.Include("TarifModels");
            aboViewModels.TypeAbo = allTypeAbonnement.ToList();
            aboViewModels.TypeAboPossede = new List<TypeAbonnementModels>();
            CarteModels carteUser = carte.findCarteByUser(User.Identity.Name);
            List<AbonnementModels> aboUserList = carteUser.AbonnementsModels.ToList();
            if(aboUserList != null)
            {
                foreach(AbonnementModels abo in aboUserList)
                {
                    int typeAboId = abo.TypeAbonnementModelsID;
                    TypeAbonnementModels typeAbo = bdd.TypeAbonnementModels.Find(typeAboId);
                    if (typeAbo != null)
                    {
                        string intitule = typeAbo.Intitule;
                        aboViewModels.TypeAboPossede.Add(typeAbo);
                        aboViewModels.TypeAbo.Remove(typeAbo);
                    }
                }
            }           
            return View(aboViewModels);
        }

        // POST: Abonnement/Create
        [HttpPost]
        public ActionResult Souscrire(AbonnementViewModels abo, string nameOfType)
        {
            try
            {
                CarteModels carteUser = carte.findCarteByUser(User.Identity.Name);
                AbonnementModels abonnement = new AbonnementModels
                {
                    TypeAbonnementModelsID = bdd.TypeAbonnementModels.ToList().Find(t => t.Intitule.Contains(nameOfType)).Id,
                    CarteModelsID = carteUser.Id,
                };
                bdd.AbonnementModels.Add(abonnement);
                //carteUser.AbonnementsModels = carteUser.AbonnementsModels.Add();
                bdd.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Abonnement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Abonnement/Edit/5
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

        // GET: Abonnement/Delete/5
        public ActionResult Supprimer(int id)
        {
            return View();
        }

        // POST: Abonnement/Delete/5
        [HttpPost]
        public ActionResult Supprimer(AbonnementViewModels aboviewmodel, string nameOfType)
        {
            try
            {
                CarteModels carteUser = carte.findCarteByUser(User.Identity.Name);
                List<AbonnementModels> aboList = carteUser.AbonnementsModels.ToList();
                foreach(AbonnementModels abo in aboList)
                {
                    int typeId = abo.TypeAbonnementModelsID;
                    TypeAbonnementModels typeAbo = bdd.TypeAbonnementModels.Find(typeId);
                    string intitule = typeAbo.Intitule;
                    if(intitule.Equals(nameOfType))
                    {
                        bdd.AbonnementModels.Remove(abo);
                    }
                }
                bdd.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
