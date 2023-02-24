using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class SelectDynamicWidgetParser : IDynamicWidgetPropertyParser
{
    public string Name => "select";

    public Task<string> ParseAsync(IHtmlHelper helper, string name, string existingValue,
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

        return Task.FromResult(existingValue);
    }
}