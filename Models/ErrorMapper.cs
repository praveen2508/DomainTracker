using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public interface IErrorMapper
    {
        List<ResultDetail> Map(List<Error> errors);
        List<ResultDetail> Map(ValidationResult validationResult);
    }
    public class ErrorMapper : IErrorMapper
    {
        public List<ResultDetail> Map(List<Error> errors)
        {
            if (errors == null || !errors.Any()) return null;

            List<ResultDetail> response = new List<ResultDetail>();

            foreach (var error in errors)
            {
                response.Add(new ResultDetail
                {
                    Detail = error.Detail,
                    Message = error.Message,
                    Type = error.Type
                });
            }

            return response;
        }

        public List<ResultDetail> Map(ValidationResult validationResult)
        {
            if (validationResult == null || validationResult.Errors == null || !validationResult.Errors.Any()) return null;

            List<ResultDetail> response = new List<ResultDetail>();

            foreach (var error in validationResult.Errors)
            {
                response.Add(new ResultDetail
                {
                    Message = error.ErrorMessage,
                });
            }

            return response;
        }
    }
}
