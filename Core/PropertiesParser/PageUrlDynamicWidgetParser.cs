using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Entities.Documents.Web;
using MrCMS.Services;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class PageUrlDynamicWidgetParser : IDynamicWidgetPropertyParser
{
    private readonly IWebpageUIService _webpageUiService;


    public PageUrlDynamicWidgetParser(IWebpageUIService webpageUiService)
    {
        _webpageUiService = webpageUiService;
    }

    public string Name => "pageUrl";

    public async Task<string> ParseAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        if (int.TryParse(existingValue, out var pageId))
        {
            var page  = await _webpageUiService.GetPage<Webpage>(pageId);
            return $"/{page?.UrlSegment}";
        }
        
        return existingValue;
    }
}