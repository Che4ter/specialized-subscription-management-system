using essentialAdmin.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace essentialAdmin.Models.TemplateViewModels
{
    public class TemplateViewModel
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        public static TemplateViewModel CreateFromTemplate(Templates p)
        {
            return new TemplateViewModel()
            {
                Id = p.Id,
                Name = p.Name,             
            };
        }
    }
}
