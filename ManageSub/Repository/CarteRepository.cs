using ManageSub.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageSub.Models;
using Microsoft.AspNet.Identity;
using ManageSub.DAL;
using ManageSub.App_Start;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ManageSub.Repository
{
    public class CarteRepository : ICarteRepository
    {
        public UserManager<IdentityModels> userManager;
        private ManageSubContext manageSubContext;

        public CarteRepository(ManageSubContext manageSubContext, UserManager<IdentityModels> aum)
        {
            this.manageSubContext = manageSubContext;
            this.userManager = aum;
        }

        public CarteModels findCarteByUser(string email)
        {
            IdentityModels userIdentity = userManager.FindByEmail(email);
            if (userIdentity != null)
            {
                var cartes = userIdentity.CarteModels;
                if (cartes != null && cartes.Count > 0)
                {
                    return cartes.First();
                }
            }
            throw new Exception("L'utilisateur n'a pas de carte");
        }
    }
}