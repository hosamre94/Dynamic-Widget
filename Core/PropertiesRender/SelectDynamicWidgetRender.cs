using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class SelectDynamicWidgetRender : IDynamicWidgetPropertyRender
{
    public string Name => "select";

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

        var values = new List<string>();
        foreach (var attr in attributes)
        {
            switch (attr.Key)
            {
                case "values":
                    var items = attr.Value.Split(",".ToCharArray(),
                        StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                    if (items.Any())
                    {
                        values.AddRange(items);
                    }

                    break;
            }
        }


        var tagBuilder = new TagBuilder("select")
        {
            Attributes =
            {
                ["id"] = TagBuilder.CreateSanitizedId(name, "-"),
                ["name"] = name,
                ["data-dynamic-input"] = null
            }
        };


        tagBuilder.AddCssClass("form-control");

        foreach (var value in values)
        {
            var option = new TagBuilder("option")
            {
                Attributes =
                {
                    [value] = value
                }
            };

            if (value == existingValue)
            {
                option.Attributes.Add("selected", "selected");
            }

            option.InnerHtml.Append(value);

            tagBuilder.InnerHtml.AppendHtml(option);
        }

        return await Task.FromResult(tagBuilder);
    }
}