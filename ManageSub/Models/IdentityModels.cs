using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ManageSub.Models
{
    public class IdentityModels : IdentityUser
    {
        public IdentityModels () {
            CarteModels = new List<CarteModels>();
        }
        public virtual ICollection<CarteModels> CarteModels { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<IdentityModels> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }
}