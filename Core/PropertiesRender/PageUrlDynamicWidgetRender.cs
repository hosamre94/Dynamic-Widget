using System;
using System.Threading.Tasks;
using MrCMS.Entities.Documents.Web;
using MrCMS.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Helpers;
using MrCMS.Web.Apps.DynamicWidget.Models;
using NHibernate;
using NHibernate.Linq;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class PageUrlDynamicWidgetRender : IDynamicWidgetPropertyRender
{
    private readonly ISession _session;
    private readonly IWebpageUIService _webpageUiService;


    public PageUrlDynamicWidgetRender(ISession session,IWebpageUIService webpageUiService)
    {
        _session = session;
        _webpageUiService = webpageUiService;
    }

    public string Name => "pageUrl";
    
    public string ResponsiveClass => "col-md-6 col-lg-4 col-xl-3";

    public async Task<IHtmlContent> RenderAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {

        var elementId =TagBuilder.CreateSanitizedId(name, "-").GetTidyFileName();
        elementId = $"{elementId}-{(new Random()).NextInt64(long.MinValue, long.MaxValue)}";
        
        var tagBuilder = new TagBuilder("select")
        {
            Attributes =
            {
                ["id"] = elementId,
                ["name"] = name,
                ["data-webpage-url-selector"] = null,
                ["data-dynamic-input"] = null
            }
        };

        Webpage page = null;

        if (string.IsNullOrEmpty(existingValue))
        {
            page = await _session.Query<Webpage>()
                .FirstOrDefaultAsync(x => x.WebpageType.EndsWith(name));
        }
        else if(int.TryParse(existingValue,out var pageId))
        {
            page = await _webpageUiService.GetPage<Webpage>(pageId);
        }

        if (page != null)
        {
            tagBuilder.InnerHtml.AppendHtml($"<option value='{page.Id}' selected>{page.Name}</option>");
        }
        
        tagBuilder.AddCssClass("form-control");

        return await Task.FromResult(tagBuilder);
    }
}