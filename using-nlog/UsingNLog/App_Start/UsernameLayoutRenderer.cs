using NLog;
using NLog.LayoutRenderers;
using System.Text;
using System.Web;

namespace UsingNLog.App_Start
{
    [LayoutRenderer("username")]
    public class UsernameLayoutRenderer : LayoutRenderer
    {
        protected override void Append(StringBuilder builder, LogEventInfo logEvent)
        {
            string username = HttpContext.Current.Session["login"]?.ToString();
            if (string.IsNullOrEmpty(username)) username = "anonymous";

            builder.Append(username);
        }
    }
}