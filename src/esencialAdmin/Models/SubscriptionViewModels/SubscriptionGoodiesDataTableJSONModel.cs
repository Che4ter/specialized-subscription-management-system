using System;

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