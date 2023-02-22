using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Helpers;
using MrCMS.Services.Resources;
using MrCMS.Web.Apps.DynamicWidget.Entities;
using NHibernate;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;

public class GetHtmlTemplateOptions : IGetHtmlTemplateOptions
{
    private readonly ISession _session;
    private readonly IStringResourceProvider _stringResourceProvider;

    public GetHtmlTemplateOptions(ISession session, IStringResourceProvider stringResourceProvider)
    {
        _session = session;
        _stringResourceProvider = stringResourceProvider;
    }

    public async Task<IList<SelectListItem>> GetOptions(Func<HtmlTemplate, Task<bool>> selected = null,
        string emptyItemText = null)
    {
        var templates = await _session.QueryOver<HtmlTemplate>()
            .OrderBy(template => template.Name).Asc
            .Cacheable().ListAsync();
        return await templates
            .BuildSelectItemListAsync(template => Task.FromResult(template.Name),
                template => Task.FromResult(template.Id.ToString()),
                selected,
                emptyItemText);
    }
}