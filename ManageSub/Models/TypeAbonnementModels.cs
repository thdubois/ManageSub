using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManageSub.Models
{
    public class TypeAbonnementModels
    {
        public int Id { get; set; }
        [Display(Name = "Intitule")]
        public string Intitule { get; set; }
        [Display(Name = "Condition")]
        public string Conditon { get; set; }
        public int TarifModelsID { get; set; }

        public ICollection<AbonnementModels> AbonnementModels { get; set; }
        public TarifModels TarifModels { get; set; }
    }
}