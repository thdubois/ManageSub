namespace ManageSub.Migrations
{
    using App_Start;
    using DAL;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ManageSub.DAL.ManageSubContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ManageSub.DAL.ManageSubContext";
        }

        protected override void Seed(ManageSub.DAL.ManageSubContext context)
        {
            var roleManager = new RoleManager<RoleModels>(new RoleStore<RoleModels>(new ManageSubContext()));
            var roleresult = roleManager.Create(new RoleModels("user"));
            var roleresult2 = roleManager.Create(new RoleModels("admin"));
            var tarifs = new List<TarifModels>
            {
            new TarifModels{Id=400, prix=10.0, type="abonnement"},
            new TarifModels{Id=402, prix=20.0, type="abonnement"},
            new TarifModels {Id=403, prix=1.70, type="ticket"},
            new TarifModels {Id=404, prix=1.40, type="ticket"}
            };
            tarifs.ForEach(s => context.TarifModels.Add(s));

            var typeAbo = new List<TypeAbonnementModels>
            {
                new TypeAbonnementModels {Id = 500,Intitule="Etudiant", Conditon="Posseder une carte étudiant", TarifModelsID= tarifs.First().Id},
                new TypeAbonnementModels {Id = 501,Intitule="Normal", Conditon="Pas de condition", TarifModelsID= tarifs.Last().Id}
            };
            typeAbo.ForEach(s => context.TypeAbonnementModels.Add(s));

            

            if (!(context.Users.Any(u => u.UserName == "thomas@gmail.com")))
            {
                var userStore = new UserStore<IdentityModels>(context);
                var userManager = new UserManager<IdentityModels>(userStore);
                var userToInsert2 = new IdentityModels { UserName = "thomas@gmail.com", PhoneNumber = "0797697898", Email = "thomas@gmail.com", EmailConfirmed = true };
                userManager.Create(userToInsert2, "azerty");
                userManager.AddToRole(userToInsert2.Id, "user");

                var carte = new List<CarteModels>
            {
                new CarteModels {Id = 200, IdentityModels = userToInsert2 ,dateCreation = DateTime.Parse("2015-09-01")}
            };
                carte.ForEach(s => context.CarteModels.Add(s));
                var ticketList = new List<TicketModels>
            {
                new TicketModels {Id = 700, CarteModelsID = carte.First().Id, TarifModelsID = tarifs.ElementAt(2).Id}
            };
                ticketList.ForEach(s => context.TicketModels.Add(s));

                var abos = new List<AbonnementModels>
            {
                new AbonnementModels {AbonnementModelsId = 100, finDeValidite= DateTime.Now.AddYears(1) ,TypeAbonnementModelsID = typeAbo.First().Id, CarteModelsID = carte.First().Id},
                new AbonnementModels {AbonnementModelsId = 101,finDeValidite= DateTime.Now.AddYears(1)  ,TypeAbonnementModelsID = typeAbo.Last().Id, CarteModelsID = carte.First().Id}
            };
                abos.ForEach(s => context.AbonnementModels.Add(s));

            }

        


            if (!(context.Users.Any(u => u.UserName == "admin")))
            {
                var userStore = new UserStore<IdentityModels>(context);
                var userManager = new UserManager<IdentityModels>(userStore);
                var userToInsert = new IdentityModels { UserName = "admin@gmail.com", PhoneNumber = "0897876565", Email = "admin@gmail.com", EmailConfirmed = true };
                userManager.Create(userToInsert, "azerty");
                userManager.AddToRole(userToInsert.Id, "admin");

                var carte = new List<CarteModels>
            {
                new CarteModels {Id = 201, IdentityModels = userToInsert ,dateCreation = DateTime.Parse("2015-09-24")}
            };
                carte.ForEach(s => context.CarteModels.Add(s));
            }
            context.SaveChanges();
        }
    }
}
