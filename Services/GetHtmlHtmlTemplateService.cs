using System.Threading.Tasks;
using MrCMS.Web.Apps.DynamicWidget.Entities;
using NHibernate;

namespace MrCMS.Web.Apps.DynamicWidget.Services;

public class GetHtmlHtmlTemplateService : IGetHtmlTemplateService
{
    private readonly ISession _session;

    public GetHtmlHtmlTemplateService(ISession session)
    {
        _session = session;
    }

    public async Task<HtmlTemplate> Get(int id)
    {
        return await _session.GetAsync<HtmlTemplate>(id);
    }
}