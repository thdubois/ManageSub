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
        public DbSet<AbonnementModels> AbonnementModels { get; set; }
        public DbSet<CarteModels> CarteModels { get; set; }
        public DbSet<TarifModels> TarifModels { get; set; }
        public DbSet<TicketModels> TicketModels { get; set; }
        public DbSet<TypeAbonnementModels> TypeAbonnementModels { get; set; }
        




        public ManageSubContext() : base("ManageSubContext")
        {
            Database.SetInitializer<ManageSubContext>(new DropCreateDatabaseIfModelChanges<ManageSubContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            /*modelBuilder.Entity<CarteModels>()
                .HasMany<AbonnementModels>(s => s.AbonnementsModels)
                .WithMany(c => c.CarteModels)
                .Map(cs =>
                {
                    cs.MapLeftKey("CarteModelsId");
                    cs.MapRightKey("AbonnementModelsId");
                    cs.ToTable("CarteAbonnement");
                });*/
        }

        public static ManageSubContext Create()
        {
            return new ManageSubContext();
        }      
    }
}