using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Data
{
    interface ITrackableEntity
    {
        DateTime? DateCreated { get; set; }
        string UserCreated { get; set; }
        DateTime? DateModified { get; set; }
        string UserModified { get; set; }
    }
}
