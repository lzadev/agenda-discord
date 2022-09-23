using Agenda.Response;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace Agenda.Helpers
{
    public static class ErrorsFromValidationResult
    {
        public static List<Error> GetErrorsDetails(IEnumerable<ValidationFailure> errors)
        {
            var errorsDetails = new List<Error>();
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    errorsDetails.Add(new Error { Message = error.ErrorMessage , Code = "BadRequest"});
                }
            }

            return errorsDetails;
        }
    }
}
