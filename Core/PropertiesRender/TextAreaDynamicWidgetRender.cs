using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class TextAreaDynamicWidgetRender : IDynamicWidgetPropertyRender
{
    public string Name => "textarea";
    
    public string ResponsiveClass => "col-12";

    public async Task<IHtmlContent> RenderAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        var tagBuilder = new TagBuilder("textarea")
        {
            TagRenderMode = TagRenderMode.Normal,
            Attributes =
            {
                ["type"] = "text",
                ["name"] = name,
                ["data-dynamic-input"] = null
            }
        };
        tagBuilder.InnerHtml.Append(existingValue);

        var elementId = TagBuilder.CreateSanitizedId(name, "-");
        tagBuilder.Attributes["id"] = elementId;

        tagBuilder.AddCssClass("form-control");
        return await Task.FromResult(tagBuilder);
    }
}