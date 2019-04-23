using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCodeGenerator.Models
{

    public class EFGeneratorObject
    {
        public List<Entity> Entities { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Entity
    {
        public string Name { get; set; }
        public string TableName { get; set; }
        public string Schema { get; set; }
        public List<Prop> Props { get; set; }
    }

    public class Prop
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool Key { get; set; }
        public bool Identity { get; set; }
        public bool Required { get; set; }
        public string NavigationType { get; set; }
    }

    public class Error
    {
        public string Type { get; set; }
        public string Format { get; set; }
    }

}