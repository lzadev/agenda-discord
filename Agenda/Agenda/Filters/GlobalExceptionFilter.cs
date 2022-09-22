using Agenda.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Agenda.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;


            if (exceptionType == typeof(NotFoundException))
            {
                var exception = (NotFoundException)context.Exception;
                context.Result = new ObjectResult(new { message = exception.Message });
                context.HttpContext.Response.StatusCode = (int)StatusCodes.Status404NotFound;
                context.ExceptionHandled = true;
                return;
            }

            if (exceptionType == typeof(BadRequestException))
            {
                var exception = (BadRequestException)context.Exception;
                context.Result = new ObjectResult(new { message = exception.Message });
                context.HttpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
                context.ExceptionHandled = true;
                return;
            }

            //UnKnown Exception
            context.Result = new ObjectResult(new { message = context.Exception.Message });
            context.ExceptionHandled = true;
        }

    }
}
