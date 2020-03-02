using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food.API.Models
{
    public class ErrorSimulatorOptions
    {
        public double ErrorRate { get; set; }
        public int ErrorStatusCode { get; set; }
    }

    public class DelaySimulatorOptions
    {
        public double DelayRate { get; set; }
        public int DelayAverageMs { get; set; }
    }
}
