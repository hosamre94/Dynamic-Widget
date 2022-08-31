using System.Linq;
using System.Threading.Tasks;
using MrCMS.Entities.Documents.Web;
using MrCMS.Helpers;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models;
using NHibernate;
using NHibernate.Linq;
using X.PagedList;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;

public class WebpageUrlSelectorService : IWebpageUrlSelectorService
{
    private readonly ISession _session;

    public WebpageUrlSelectorService(ISession session)
    {
        _session = session;
    }

    public async Task<IPagedList<UrlSelectorLookupResult>> Search(string term, int page)
    {
        return await _session.Query<Webpage>()
            .Where(x => x.UrlSegment.Like($"%{term}%") || x.Name.Like($"%{term}%"))
            .OrderByDescending(x => x.CreatedOn)
            .Select(x => new UrlSelectorLookupResult
            {
                id = "/" + x.UrlSegment,
                text = x.Name
            })
            .PagedAsync(page);
    }
}