using System.ComponentModel.DataAnnotations;

namespace SampleMvcApp.Web.Core.Mvc.Attributes
{
    public class UICheckboxAttribute : UIHintAttribute
    {
        public UICheckboxAttribute(string propName)
          : base("UICheckbox", "MVC")
        {
            PropName = propName;
        }

        public string PropName { get; private set; }
    }
}