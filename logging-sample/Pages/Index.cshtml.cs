using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoggingSample.Pages
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
            _logger.LogInformation($"Index page visited. Datetime : {DateTime.Now.ToLongTimeString()}");
            _logger.LogError($"Index page error. Datetime : {DateTime.Now.ToLongTimeString()}");
            
        }
    }
}