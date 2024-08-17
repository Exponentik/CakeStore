using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CakeStore.Api.Settings;
using CakeStore.Common;
using CakeStore.Services.Settings;
using System.Reflection;

namespace CakeStore.Api.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public bool OpenApiEnabled => settings.Enabled;

        [BindProperty]
        public string Version => Assembly.GetExecutingAssembly().GetAssemblyVersion();


        [BindProperty]
        public string HelloMessage => apiSettings.HelloMessage;


        private readonly SwaggerSettings settings;
        private readonly ApiSpecialSettings apiSettings;

        public IndexModel(SwaggerSettings settings, ApiSpecialSettings apiSettings)
        {
            this.settings = settings;
            this.apiSettings = apiSettings;
        }

        public void OnGet()
        {

        }
    }
}