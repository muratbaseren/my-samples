using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcAppWithVue
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class VueData : Attribute
    {
        public VueData(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public interface IVueParser
    {
        Dictionary<string, object> ParseData<TModel>(TModel model);
    }

    public class VueParser : IVueParser
    {
        public Dictionary<string, object> ParseData<TModel>(TModel model)
        {
            var props = model.GetType().GetProperties();
            var result = new Dictionary<string, object>();

            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<VueData>();

                if (attr != null)
                {
                    result.Add(attr.Name, prop.GetValue(model));
                }
            }

            return result;
        }
    }
}