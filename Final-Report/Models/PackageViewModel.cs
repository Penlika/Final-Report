using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Report.Models
{
    public class PackagesViewModel
    {
        public IEnumerable<HOTEL> Hotels { get; set; }
        public IEnumerable<FLIGHT> Flights { get; set; }
    }
}