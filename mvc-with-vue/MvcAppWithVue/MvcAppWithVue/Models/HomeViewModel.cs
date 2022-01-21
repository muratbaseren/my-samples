using System.Collections.Generic;

namespace MvcAppWithVue
{
    public class HomeViewModel
    {
        [VueData("message")]
        public string Message { get; set; } = "Hello from Vue!";

        [VueData("menu")]
        public List<string> MenuItems { get; set; } = new List<string>()
        {
            "Menu Item 1",
            "Menu Item 2",
        };

        public string RazorMessage { get; set; } = "Hello from Razor!";

        public Dictionary<string, object> VueData { get; set; } = new Dictionary<string, object>();
    }
}