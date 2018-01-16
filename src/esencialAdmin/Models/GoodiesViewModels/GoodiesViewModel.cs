using esencialAdmin.Data.Models;
using System.ComponentModel;

namespace esencialAdmin.Models.GoodiesViewModels
{
    public class GoodiesViewModel
    {
        [DisplayName("ID")]
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        public static GoodiesViewModel CreateFromGoody(PlanGoodies p)
        {
            return new GoodiesViewModel()
            {
                Id = p.Id,
                Name = p.Name,             
            };
        }
    }
}