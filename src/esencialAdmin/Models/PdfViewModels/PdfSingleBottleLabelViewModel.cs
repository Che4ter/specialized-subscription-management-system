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
    public class PdfSingleBottleLabelViewModel
    {
        public String Name { get; set; }

        public String Nr { get; set; }

        public String Typ { get; set; }

        public static PdfSingleBottleLabelViewModel Create(String Name, String Nr, String Typ)
        {
            var newModel = new PdfSingleBottleLabelViewModel()
            {
                Name = Name,
                Nr = Nr,
                Typ = Typ

            };

          
            return newModel;
        }

    }
}
