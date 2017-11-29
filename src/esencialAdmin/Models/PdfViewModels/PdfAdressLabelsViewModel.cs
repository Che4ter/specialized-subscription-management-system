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
    public class PdfAdressLabelsViewModel
    {
        public List<PdfSingleAdressViewModel> adresses { get; set; }
    }
}
