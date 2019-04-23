using EFCodeGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EFCodeGenerator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public MvcHtmlString Index(EFGeneratorObject model)
        {
            StringBuilder code = new StringBuilder();

            model.Entities.ForEach(ent =>
            {
                if (!string.IsNullOrEmpty(ent.TableName))
                    code.AppendLine($"[Table(\"{ent.TableName}\", Schema = \"{ent.Schema}\")][br]");

                code.AppendLine($"public class {ent.Name}[br]");
                code.AppendLine("{[br]");

                ent.Props.OrderByDescending(x => x.Key).ToList().ForEach(prop =>
                {
                    if (prop.Key)
                        code.AppendLine($"[p1][Key][br]");

                    if (!string.IsNullOrEmpty(prop.DisplayName))
                        code.AppendLine($"[p1][DisplayName(\"{prop.DisplayName}\")][br]");

                    if (prop.Required)
                        code.AppendLine($"[p1][Required(ErrorMessage = \"{model.Errors?.FirstOrDefault(x => x.Type == "required")?.Format}\")][br]");

                    if (string.IsNullOrEmpty(prop.NavigationType))
                    {
                        code.AppendLine($"[p1]public {prop.Type} {prop.Name} {{ get; set; }}[br][br]");
                    }
                    else
                    {
                        string type = "";
                        type = (prop.Type == "list") ? $"List&lt;{prop.NavigationType}&gt;" : "";

                        code.AppendLine($"[p1]public virtual {type} {prop.Name} {{ get; set; }}[br][br]");
                    }
                });

                code.AppendLine("}[br]");

            });


            code.AppendLine("[br]public class DatabaseContext : DbContext[br]");
            code.AppendLine("{[br]");

            model.Entities.ForEach(ent =>
            {
                code.AppendLine($"[p1]public DbSet&lt;{ent.Name}&gt; {ent.TableName} {{ get; set; }}[br]");
            });

            code.AppendLine("}[br]");

            return MvcHtmlString.Create(code.ToString().ApplyBreakingLine().ApplyPadding());
        }
    }
}