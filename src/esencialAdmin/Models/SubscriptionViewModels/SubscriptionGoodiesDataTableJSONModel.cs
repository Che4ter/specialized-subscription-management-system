using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.SubscriptionViewModels
{
    public class SubscriptionGoodiesDataTableJSONModel
    {
        public int Id { get; set; }
        public int PlantNr { get; set; }
        public String Customer { get; set; }
        public String Plan { get; set; }
        public String Goodies { get; set; }

        public String Periode { get; set; }          
        public String Status { get; set; }

    }
}
