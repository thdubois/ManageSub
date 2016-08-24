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
        public int nbTicket { get; set; }
    }
}