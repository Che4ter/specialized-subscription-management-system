using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Models.CustomerViewModels
{
    public class PlanViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PurchasesRemarks { get; set; }
        public string GeneralRemarks { get; set; }
        public DateTime? DateCreated { get;  }
        public string UserCreated { get;  }
        public DateTime? DateModified { get;  }
        public string UserModified { get;  }
    }
}
