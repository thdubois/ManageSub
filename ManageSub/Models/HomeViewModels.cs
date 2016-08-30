using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageSub.Models
{
    public class HomeViewModels
    {
        [Required]
        [Display(Name = "Nombre de ticket")]
        public int nbTicket{ get; set; }

        [Required]
        [Display(Name = "Nombre d'abonnement")]
        public int nbAbo { get; set; }

        [Required]
        [Display(Name = "Liste de vos abonnements")]
        public List<string> intituleList { get; set; }
    }
}