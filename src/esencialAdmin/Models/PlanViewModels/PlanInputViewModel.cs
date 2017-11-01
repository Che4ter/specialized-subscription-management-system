using esencialAdmin.Data.Models;
using esencialAdmin.Models.TemplateViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.PlanViewModels
{
    public class PlanInputViewModel
    {

        [DisplayName("ID")]
        public int ID { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Preis")]
        public decimal? Price { get; set; }

        [Required]
        [DisplayName("Dauer(in Jahren)")]
        public int? Duration { get; set; }

        [Required]
        [DisplayName("Stichtag")]
        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }

        [Required]
        [DisplayName("Etiketten Vorlage")]
        public int? TemplateID { get; set; }

        [DisplayName("Erstellt am")]
        public string DateCreated { get; set; }

        [DisplayName("Erstellt durch")]
        public string UserCreated { get; set; }

        [DisplayName("Zuletzt bearbeitet am")]
        public string DateModified { get; set; }

        [DisplayName("Zuletzt bearbeitet durch")]
        public string UserModified { get; set; }

        [Display(Name = "Etiketten Vorlagen")]
        public virtual List<TemplateViewModel> Templates { get; set; }

        public static PlanInputViewModel CreateFromPlan(Plans p)
        {
            var newModel = new PlanInputViewModel()
            {
                ID = p.Id,
                Name = p.Name,
                Price = p.Price,
                Duration = p.Duration,
                Deadline = p.Deadline,
                TemplateID = p.FkTemplateLabel,              
                UserCreated = p.UserCreated,
                UserModified = p.UserModified               
            };

            if (p.DateCreated != null)
            {
                newModel.DateCreated = p.DateCreated.Value.ToLocalTime().ToString();
            }
            if (p.DateModified != null)
            {
                newModel.DateModified = p.DateModified.Value.ToLocalTime().ToString();
            }
            return newModel;
        }
    }
}
