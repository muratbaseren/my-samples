using System.Collections.Generic;

namespace SampleMvcApp.WebApp.Substructure
{
    public class JsonResultObject<T>
    {
        public bool HasError { get; set; }
        public List<FieldException> ErrorMessages { get; set; }
        public string ResultMessage { get; set; }
        public bool IsRedirecting { get; set; }
        public string RedirectionUrl { get; set; }
        public T Data { get; set; }
    }
}