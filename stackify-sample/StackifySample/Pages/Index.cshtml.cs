using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StackifySample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            try
            {
                StackifyLib.Logger.Queue("DEBUG", "Entering Home-Index()");
                StackifyLib.Logger.QueueException(new Exception("Bu bir deneme hatasıdır."));
            }
            catch (Exception ex)
            {
                StackifyLib.Logger.QueueException(ex);
            }
        }
    }
}