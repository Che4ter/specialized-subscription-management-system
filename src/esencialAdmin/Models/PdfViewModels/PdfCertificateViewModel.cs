using System;
using System.ComponentModel;

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