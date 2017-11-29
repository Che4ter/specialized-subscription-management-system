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
    public class PdfCertificateViewModel
    {
        [DisplayName("Nr")]
        public int PlantNumber { get; set; }

        public String Customer { get; set; }
       
        public int LastYear { get; set; }

    }
}
