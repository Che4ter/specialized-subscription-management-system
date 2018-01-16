using esencialAdmin.Models.GoodiesViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionInputViewModel
    {

        [DisplayName("ID")]
        public int ID { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Preis")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Dauer(in Jahren)")]
        public int Duration { get; set; }

        [Required]
        [DisplayName("Stichtag")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Required]
        [DisplayName("Ernteanteil")]
        public int GoodyID { get; set; }

        [DisplayName("Erstellt am")]
        public string DateCreated { get; set; }

        [DisplayName("Erstellt durch")]
        public string UserCreated { get; set; }

        [DisplayName("Zuletzt bearbeitet am")]
        public string DateModified { get; set; }

        [DisplayName("Zuletzt bearbeitet durch")]
        public string UserModified { get; set; }

        [Display(Name = "Etiketten Vorlagen")]
        public virtual List<GoodiesViewModel> Goodies { get; set; }
    }
}