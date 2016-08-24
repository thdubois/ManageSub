using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageSub.Models
{
    public class TicketModels
    {
        public int TicketModelsID { get; set; }
        public virtual CarteModels CarteModels { get; set; }
    }
}