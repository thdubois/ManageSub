using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageSub.Models
{
    public class AbonnementViewModels
    {
        [Required]
        [Display(Name = "Type d'abonnement")]
        public List<TypeAbonnementModels> TypeAbo { get; set; }

        [Required]
        [Display(Name = "Abonnement que possède déjà l'utilisateur")]
        public List<TypeAbonnementModels> TypeAboPossede { get; set; }
    }
}