using esencialAdmin.Data.Models;
using System.ComponentModel;

namespace esencialAdmin.Models.TemplateViewModels
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