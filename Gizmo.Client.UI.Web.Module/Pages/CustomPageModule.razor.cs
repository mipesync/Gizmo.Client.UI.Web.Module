using Gizmo.UI;
using Gizmo.Web.Api.Models.Models.API.Request.CustomPage;
using Gizmo.Web.Components;
using Microsoft.AspNetCore.Components;

namespace Gizmo.Client.UI.Web.Module.Pages
{
    [ModuleGuid(KnownModules.MODULE_WEB)]
    [PageUIModule(Title = "Web", Description = "Web page")]
    [ModuleDisplayOrder(int.MaxValue)]
    [Route("/web")]
    public partial class CustomPageModule : CustomDOMComponentBase
    {
        [Inject] 
        private IGizmoClient GizmoClient { get; set; } = null!;
        
        [CascadingParameter]
        public Action<bool> SetBackgroundVisible { get; set; } = null!;
        
        private CustomPageModel? Page { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var pages = await GizmoClient.CustomPagesGetAsync();
        
            Page = pages.FirstOrDefault(p => string.Equals(KnownModules.MODULE_WEB, p.ModuleId.ToString().ToLower(), StringComparison.OrdinalIgnoreCase));
        
            if (Page != null)
            {
                var visible = !Page.IsCustomTemplate;
                SetBackgroundVisible?.Invoke(visible);
            }
        }
        
        public override void Dispose()
        {
            SetBackgroundVisible?.Invoke(true);
            base.Dispose();
        }
    }
}
