using System.ComponentModel.DataAnnotations;

namespace SampleMvcApp.WebApp.Substructure.Attributes
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