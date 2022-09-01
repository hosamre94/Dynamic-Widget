using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Entities.Documents.Web;
using MrCMS.Helpers;
using MrCMS.Services;
using MrCMS.Services.Resources;
using MrCMS.Web.Apps.DynamicWidget.Models;
using NHibernate;
using NHibernate.Linq;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class PageAnchorTagDynamicWidgetParser : IDynamicWidgetPropertyParser
{
    private readonly IWebpageUIService _webpageUiService;


    public PageAnchorTagDynamicWidgetParser(IWebpageUIService webpageUiService)
    {
        _webpageUiService = webpageUiService;
    }

    public string Name => "pageAnchorTag";

    public async Task<string> ParseAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        if (int.TryParse(existingValue, out var pageId))
        {
            var page  = await _webpageUiService.GetPage<Webpage>(pageId);
            
            var anchorTag = new TagBuilder("a");
            anchorTag.Attributes.Add("href",$"/{page?.UrlSegment}");
            anchorTag.InnerHtml.Append(page?.Name ?? string.Empty);

            if (attributes != null)
            {
                foreach (var attr in attributes)
                {
                    anchorTag.Attributes.Add(attr.Key,attr.Value);
                }
            }

            using var writer = new StringWriter();
            anchorTag.WriteTo(writer, HtmlEncoder.Default);

            return writer.ToString();
        }
        
        return existingValue;
    }
}