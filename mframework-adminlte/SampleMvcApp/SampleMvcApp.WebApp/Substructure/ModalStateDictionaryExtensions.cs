using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SampleMvcApp.WebApp.Substructure
{
    public static class ModelStateDictionaryExtensions
    {
        public static List<FieldException> GetExceptionMessageList(this ModelStateDictionary dictionary)
        {
            List<FieldException> errorMessages = new List<FieldException>();

            dictionary
                .Where(item => item.Value.Errors.Count > 0)
                .ToList()
                .ForEach(item =>
                    errorMessages.AddRange(
                        item.Value.Errors.Select(y =>
                                                new FieldException()
                                                {
                                                    FieldName = item.Key,
                                                    ErrorMessage = y.ErrorMessage
                                                })));

            return errorMessages;
        }
    }

    public class FieldException
    {
        public string FieldName { get; set; }
        public string ErrorMessage { get; set; }
    }
}