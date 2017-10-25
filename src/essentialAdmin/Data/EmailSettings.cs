using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace essentialAdmin.Data
{
    public class EmailSettings
    {
        public String Domain { get; set; }

        public int Port { get; set; }

        public String UsernameEmail { get; set; }

        public String UsernamePassword { get; set; }

        public String DisplayName { get; set; }

        public String FromEmail { get; set; }

        public String ToEmail { get; set; }

        public String CcEmail { get; set; }
    }
}
