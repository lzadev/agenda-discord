using Agenda.Exceptions;
using Agenda.Helpers;
using Agenda.Response;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Agenda.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception.GetType();
            context.HttpContext.Response.ContentType = "application/json";

            if (exceptionType == typeof(NotFoundException))
            {
                context.Result = new ObjectResult(GetResponseError("NotFound", context.Exception.Message, exceptionType));
                context.HttpContext.Response.StatusCode = (int)StatusCodes.Status404NotFound;
                context.ExceptionHandled = true;
                return;
            }

            if (exceptionType == typeof(BadRequestException))
            {
                context.Result = new ObjectResult(GetResponseError("BadRequest", context.Exception.Message, exceptionType));
                context.HttpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
                context.ExceptionHandled = true;
                return;
            }

            if (context.Exception.GetType() == typeof(ValidationException))
            {
                var exception = (ValidationException)context.Exception;
                var errors = ErrorsFromValidationResult.GetErrorsDetails(exception.Errors);
                context.Result = new ObjectResult(GetResponseError("BadRequest", context.Exception.Message, exceptionType, errors));
                context.HttpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;
                context.ExceptionHandled = true;
                return;
            }

            //UnKnown Exception
            context.HttpContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(GetResponseError("InternalServerError", context.Exception.Message, exceptionType));
            context.ExceptionHandled = true;
        }


        private ApiResponse<Error> GetResponseError(string code, string message, Type exceptionType, List<Error> fluentValidationErrors = null)
        {
            var apiResponse = new ApiResponse<Error>() { Success = false };
            var errorList = new List<Error>();

            if (!(exceptionType == typeof(ValidationException)))
            {
                var error = new Error
                {
                    Code = code,
                    Message = message
                };
                errorList.Add(error);
                apiResponse.Errors = new ResponseError
                {
                    Error = errorList,
                    UnAuthorizedRequest = false
                };

                return apiResponse;
            }

            apiResponse.Errors = new ResponseError
            {
                Error = fluentValidationErrors,
                UnAuthorizedRequest = false
            };

            return apiResponse;
        }
    }
}
