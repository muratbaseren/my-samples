import $$ from "dom7";
import Framework7 from "framework7/framework7.esm.bundle.js";

// Import F7 Styles
import "framework7/css/framework7.bundle.css";

// Import Icons and App Custom Styles
import "../css/icons.css";
import "../css/app.css";

// Import Routes
import routes from "./routes.js";

// Import main app component
import App from "../app.f7.html";

var app = new Framework7({
  root: "#app", // App root element
  component: App, // App main component

  name: "testomy", // App name
  theme: "auto", // Automatic theme detection
  // App root data
  data: function() {
    return {
      // Demo products for Catalog section
      products: [
        {
          id: "1",
          title: "Apple iPhone 8",
          description:
            "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nisi tempora similique reiciendis, error nesciunt vero, blanditiis pariatur dolor, minima sed sapiente rerum, dolorem corrupti hic modi praesentium unde saepe perspiciatis."
        },
        {
          id: "2",
          title: "Apple iPhone 8 Plus",
          description:
            "Velit odit autem modi saepe ratione totam minus, aperiam, labore quia provident temporibus quasi est ut aliquid blanditiis beatae suscipit odio vel! Nostrum porro sunt sint eveniet maiores, dolorem itaque!"
        },
        {
          id: "3",
          title: "Apple iPhone X",
          description:
            "Expedita sequi perferendis quod illum pariatur aliquam, alias laboriosam! Vero blanditiis placeat, mollitia necessitatibus reprehenderit. Labore dolores amet quos, accusamus earum asperiores officiis assumenda optio architecto quia neque, quae eum."
        }
      ]
    };
  },

  // App routes
  routes: routes
});
