using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageSub.Models
{
    public class TicketModels
    {
        public int Id { get; set; }
        public int CarteModelsID { get; set; }
        public int TarifModelsID { get; set; }

        public virtual TarifModels TarifModels{ get; set; }
        public virtual CarteModels CarteModels { get; set; }
    }
}