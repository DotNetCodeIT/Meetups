using Food.API.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Food.API.Filters
{
    public class DelaySimulatorFilter : IActionFilter
    {
        private readonly IOptionsSnapshot<DelaySimulatorOptions> _delayOptions;

        public DelaySimulatorFilter(IOptionsSnapshot<DelaySimulatorOptions> delayOptions)
        {
            _delayOptions = delayOptions;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var randomValue = new Random().NextDouble();

            if (randomValue < _delayOptions.Value.DelayRate)
            {
                var averageDelay = _delayOptions.Value.DelayAverageMs;
                var jitter = averageDelay / 3;
                var delay = averageDelay + new Random().Next(-jitter, jitter);
                Thread.Sleep(delay);
            }
        }
    }
}
