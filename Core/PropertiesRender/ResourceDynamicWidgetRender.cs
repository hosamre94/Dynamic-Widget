using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class ResourceDynamicWidgetRender : IDynamicWidgetPropertyRender
{
    public string Name => "resource";
    
    public string ResponsiveClass => "col-md-6 col-lg-4 col-xl-3";

    public async Task<IHtmlContent> RenderAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        return await Task.FromResult(HtmlString.Empty);
    }
}