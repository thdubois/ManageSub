using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageSub.Models
{
    public class TarifModels
    {
        public int Id { get; set; }
        public double prix { get; set; }
        public string type { get; set; }

        public virtual ICollection<TicketModels> TicketModels { get; set; }
        public virtual ICollection<TypeAbonnementModels> TypeAbonnementsModels { get; set; }
    }
}