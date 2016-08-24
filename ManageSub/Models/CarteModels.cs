using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ManageSub.Models
{
    public class CarteModels
    {
        public int Id { get; set; }
        public DateTime dateCreation { get; set; }
        public virtual IdentityModels IdentityModels { get; set; }
        public virtual ICollection<TicketModels> TicketModels { get; set; }
    }
}