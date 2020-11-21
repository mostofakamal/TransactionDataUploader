using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using TransactionDataUploader.Core.Domain.Dtos;

namespace TransactionDataUploader.Core.Domain.Services
{
    public abstract class FileParser<TSource, TResult>
        where TSource : class
        where TResult : class
    {
        public DataResult<TResult> ExtractDataFromContent(string content)
        {
            // Parse 
            var dataResult = new DataResult<TResult>();
            
            var result = ParseData(content);

            //Validate
            if (result == null)
            {
                dataResult.Errors = new List<string>()
                {
                    "Data parsing failed"
                };
                return dataResult;
            }
            
            var validationErrors = ValidateData(result);

            if (validationErrors.Any())
            {
                dataResult.Errors = validationErrors;
                return dataResult;
            }
            
            // Map
            var mappedResult = Map(result);
            dataResult.Data = mappedResult;
            dataResult.Errors = validationErrors;

            return dataResult;
        }

        protected abstract List<TSource> ParseData(string content);

        private List<string> ValidateData(IList<TSource> data)
        {
            var errors = new List<string>();
            if (data == null)
            {
               throw new ArgumentException("Data to validate is null",nameof(data));
            }
            foreach (var transactionData in data)
            {
                var result = Validator.Validate(transactionData);
                if (!result.IsValid)
                {
                    errors.Add($"Error in data item:  {data.IndexOf(transactionData)} Error Details: {string.Join("\n", result.Errors.Select(x => x.ErrorMessage).ToList())} {Environment.NewLine}");
                }
            }

            return errors;
        }

        protected abstract AbstractValidator<TSource> Validator { get; }

        protected abstract List<TResult> Map(List<TSource> data);
    }
}
