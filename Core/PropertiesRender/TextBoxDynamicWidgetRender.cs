using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class TextBoxDynamicWidgetRender : IDynamicWidgetPropertyRender
{
    public string Name => "text";
    
    public string ResponsiveClass => "col-md-6 col-lg-4 col-xl-3";

    public async Task<IHtmlContent> RenderAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        if (string.IsNullOrWhiteSpace(existingValue) && attributes != null)
        {
            existingValue = attributes.Aggregate(existingValue, (current, attr) => attr.Key switch
            {
                "default" => attr.Value,
                _ => current
            });
        }
            
        var tagBuilder = new TagBuilder("input")
        {
            Attributes =
            {
                ["id"] = TagBuilder.CreateSanitizedId(name, "-"),
                ["name"] = name,
                ["type"] = "text",
                ["value"] = existingValue,
                ["data-dynamic-input"] = null
            }
        };

        tagBuilder.AddCssClass("form-control");

        return await Task.FromResult(tagBuilder);
    }
}