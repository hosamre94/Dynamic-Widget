using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MrCMS.Helpers;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models;
using MrCMS.Web.Apps.DynamicWidget.Entities;
using NHibernate;
using X.PagedList;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;

public class HtmlTemplateAdminService : IHtmlTemplateAdminService
{
    private readonly IMapper _mapper;
    private readonly ISession _session;

    public HtmlTemplateAdminService(ISession session, IMapper mapper)
    {
        _session = session;
        _mapper = mapper;
    }
    
    public async Task<IPagedList<HtmlTemplate>> SearchAsync(HtmlTemplateSearchModel searchModel)
    {
        var query = _session.Query<HtmlTemplate>();

        if (!string.IsNullOrWhiteSpace(searchModel.Name)) query = query.Where(x => x.Name.Contains(searchModel.Name));

        return await query.OrderBy(x => x.Name).PagedAsync(searchModel.Page);
    }

    public async Task AddAsync(AddHtmlTemplateModel addHtmlTemplateModel)
    {
        var htmlTemplate = _mapper.Map<HtmlTemplate>(addHtmlTemplateModel);

        await _session.TransactAsync(s => s.SaveAsync(htmlTemplate));
    }

    public async Task<HtmlTemplate> GetAsync(int id)
    {
        return await _session.GetAsync<HtmlTemplate>(id);
    }

    public async Task UpdateAsync(UpdateHtmlTemplateModel model)
    {
        var htmlTemplate = await GetAsync(model.Id);
        _mapper.Map(model, htmlTemplate);
        await _session.TransactAsync(session => session.UpdateAsync(htmlTemplate));
    }

    public async Task DeleteAsync(int id)
    {
        var htmlTemplate = await GetAsync(id);

        await _session.TransactAsync(s => s.DeleteAsync(htmlTemplate));
    }
}