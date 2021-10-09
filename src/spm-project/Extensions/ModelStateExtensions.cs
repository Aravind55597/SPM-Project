using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPM_Project.Extensions
{
    public static class ModelStateExtensions
    {

        public static Dictionary<string,string> AllErrors(this ModelStateDictionary modelState)
        {
            //var result = new Dictionary<string, string>();
            var errors = new List<Error>();
            var result = new Dictionary<string, string>(); 
            var erroneousFields = modelState.Where(ms => ms.Value.Errors.Any())
                                            .Select(x => new { x.Key, x.Value.Errors });

            foreach (var erroneousField in erroneousFields)
            {
                var fieldKey = erroneousField.Key;
                var fieldErrors = erroneousField.Errors
                                   .Select(error => new Error(fieldKey, error.ErrorMessage));
                errors.AddRange(fieldErrors);
            }

            foreach (var err in errors)
            {
                result.Add(err.Key.ToString() , err.Message.ToString()); 
            }
            return result;
        }





    }

    public class Error
    {
        public Error(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
