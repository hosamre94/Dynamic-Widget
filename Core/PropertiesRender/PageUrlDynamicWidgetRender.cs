using System.Linq;
using System.Threading.Tasks;
using MrCMS.Entities.Documents.Web;
using MrCMS.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Web.Apps.DynamicWidget.Models;
using NHibernate;
using NHibernate.Linq;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class PageUrlDynamicWidgetRender : IDynamicWidgetPropertyRender
{
    private readonly ISession _session;
    private readonly IGetCurrentPage _getCurrentPage;
    private readonly IGetWebpageByUrl<Webpage> _getWebpageByUrl;


    public PageUrlDynamicWidgetRender(ISession session, IGetCurrentPage getCurrentPage, IGetWebpageByUrl<Webpage> getWebpageByUrl)
    {
        _session = session;
        _getCurrentPage = getCurrentPage;
        _getWebpageByUrl = getWebpageByUrl;
    }

    public string Name => "pageUrl";

    public async Task<IHtmlContent> RenderAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        var tagBuilder = new TagBuilder("select")
        {
            Attributes =
            {
                ["id"] = TagBuilder.CreateSanitizedId(name, "-"),
                ["name"] = name,
                ["data-webpage-url-selector"] = null,
                ["data-dynamic-input"] = null
            }
        };

        Webpage page;

        if (string.IsNullOrEmpty(existingValue))
        {
            page = await _session.Query<Webpage>()
                .FirstOrDefaultAsync(x => x.WebpageType.EndsWith(name));
        }
        else
        {
            page = await _getWebpageByUrl.GetByUrl(existingValue.TrimStart('/'));
        }

        if (page != null)
        {
            tagBuilder.InnerHtml.AppendHtml($"<option value='/{page.UrlSegment}' selected>{page.Name}</option>");
        }
        
        tagBuilder.AddCssClass("form-control");

        return await Task.FromResult(tagBuilder);
    }
}