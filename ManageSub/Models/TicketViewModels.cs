using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageSub.Models
{
    public class TicketViewModels
    {
        [Required]
        [Display(Name = "Nombre de ticket")]
        public uint nbTicket { get; set; }

        [Required]
        [Display(Name = "Prix du ticket")]
        public double prixTicket { get; set; }

    }
}