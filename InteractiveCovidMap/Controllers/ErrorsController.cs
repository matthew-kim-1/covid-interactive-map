using CovidTracking.HelperModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracking.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : Controller
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = 500;

            //if (exception == HttpStatusException httpException) code = 404;
            //else if (exception is UnauthorizedAccessException) code = 401;
            //else if (exception is MyException) code = 400;

            Response.StatusCode = code;

            return new ErrorResponse(exception);
        }
    }
}
