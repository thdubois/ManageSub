using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageSub.Models
{
    public class AbonnementModels
    {
        [Key]
        public int AbonnementModelsId { get; set; }
        public int TypeAbonnementModelsID { get; set; }
        public int CarteModelsID { get; set; }
        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "Please enter a start date")]
        [DataType(DataType.Date)]
        public DateTime finDeValidite = DateTime.Now;
         
        public TypeAbonnementModels TypeAbonnementModels { get; set; }
        public virtual CarteModels CarteModels{ get; set; }


    }
}