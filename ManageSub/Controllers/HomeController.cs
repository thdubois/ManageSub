using ManageSub.App_Start;
using ManageSub.DAL;
using ManageSub.Interface;
using ManageSub.Models;
using ManageSub.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ManageSub.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class HomeController : Controller
    {
        private ICarteRepository carteRepository;
        private ApplicationUserManager _userManager;
        private ManageSubContext _context;

        private ManageSubContext Context
        {
            get
            {
                if(_context != null)
                {
                    return _context;
                }
                else
                {
                    return new ManageSubContext();
                }
            }
        }
        public UserManager<IdentityModels> UserManager
        {
            get
            {
                if(_userManager != null)
                {
                    return _userManager;
                }
                else
                {
                    var userStore = new UserStore<IdentityModels>(Context);
                    var userManager = new UserManager<IdentityModels>(userStore);
                    return userManager;
                }
            }
        }
        public HomeController()
        {
            this.carteRepository = new CarteRepository(new ManageSubContext(), UserManager);
        }

        public ActionResult Index()
        {
            try
            {
                HomeViewModels homeViewModel = new HomeViewModels();
                //nbTicket
                CarteModels carte = carteRepository.findCarteByUser(User.Identity.Name);
                int nbTicket = carte.TicketModels.Count;
                homeViewModel.nbTicket = nbTicket;
                //abonnement souscrit
                int nbAboSouscrit = carte.AbonnementsModels.Count;
                homeViewModel.nbAbo = nbAboSouscrit;
                var enumAbo = carte.AbonnementsModels.GetEnumerator();
                List<string> intituleList = new List<string>();
                while (enumAbo.MoveNext())
                {
                    AbonnementModels item = enumAbo.Current;
                    int idType = item.TypeAbonnementModelsID;
                    TypeAbonnementModels typeAbo = Context.TypeAbonnementModels.Find(idType);
                    intituleList.Add(typeAbo.Intitule);
                }
                homeViewModel.intituleList = intituleList;
                return View(homeViewModel);
            }
            catch(Exception e)
            {
                HomeViewModels homeView = new HomeViewModels();
                homeView.nbTicket = 0;
                homeView.nbAbo = 0;
                homeView.intituleList = new List<string>();
                return View(homeView);
            }

        }
    }
}