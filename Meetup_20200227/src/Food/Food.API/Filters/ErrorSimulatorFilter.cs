using Food.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Food.API.Filters
{

    public class ErrorSimulatorFilter : IActionFilter
    {
        private readonly IOptionsSnapshot<ErrorSimulatorOptions> _errorOptions;

        public ErrorSimulatorFilter(IOptionsSnapshot<ErrorSimulatorOptions> errorOptions)
        {
            _errorOptions = errorOptions;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var randomValue = new Random().NextDouble();

            if (randomValue < _errorOptions.Value.ErrorRate)
            {
                context.Result = new StatusCodeResult(_errorOptions.Value.ErrorStatusCode);
            }
        }
    }
}
