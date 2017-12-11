using esencialAdmin.Data.Models;
using esencialAdmin.Models.GoodiesViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.PdfViewModels
{
    public class PdfSinglePictureTemplateViewModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String PlantNr { get; set; }
    }
}
