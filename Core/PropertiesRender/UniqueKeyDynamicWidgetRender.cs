using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class UniqueKeyDynamicWidgetRender : IDynamicWidgetPropertyRender
{
    public string Name => "uniqueKey";
    
    public string ResponsiveClass => "col-12 d-none";

    public async Task<IHtmlContent> RenderAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        var id = string.IsNullOrEmpty(existingValue) ? "uk_" + Guid.NewGuid().ToString("n")[..5] : existingValue;

        IHtmlContentBuilder htmlContent = new HtmlContentBuilder();

        var tagBuilder = new TagBuilder("input")
        {
            Attributes =
            {
                ["type"] = "hidden",
                ["name"] = name,
                ["value"] = id,
                ["data-unique-key"] = null,
                ["data-dynamic-input"] = null
            }
        };

        var elementId = TagBuilder.CreateSanitizedId(name, "-");
        tagBuilder.Attributes["id"] = elementId;

        htmlContent.AppendHtml(tagBuilder);

        var label = new TagBuilder("span");
        label.InnerHtml.Append(id);
        label.AddCssClass("fw-bold");

        htmlContent.AppendHtml(label);

        return await Task.FromResult(htmlContent);
    }
}