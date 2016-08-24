using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using ManageSub.Models;

namespace ManageSub.DAL
{
    public class ManageSubContext : IdentityDbContext<IdentityModels>
    {
        public DbSet<CarteModels> CarteModels { get; set; }
        public DbSet<TicketModels> TicketModels { get; set; }


        public ManageSubContext() : base("ManageSubContext")
        {
            Database.SetInitializer<ManageSubContext>(new DropCreateDatabaseAlways<ManageSubContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }

        public static ManageSubContext Create()
        {
            return new ManageSubContext();
        }
    }
}