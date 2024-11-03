using Microsoft.AspNetCore.Components;

namespace CleanArchitectureSample.Aspire.Web.Components.Pages
{
    public class BasePage : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

    }
}
