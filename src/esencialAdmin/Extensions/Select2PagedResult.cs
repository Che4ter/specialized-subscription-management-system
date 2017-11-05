using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace esencialAdmin.Extensions
{
    public class Select2PagedResult
    {
        public int Total { get; set; }
        public List<Select2Result> Results { get; set; }
    }
}
