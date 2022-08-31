using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MrCMS.Entities.Documents.Web;
using MrCMS.Services;
using MrCMS.Services.Resources;
using MrCMS.Web.Apps.DynamicWidget.Models;
using NHibernate;
using NHibernate.Linq;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class PageUrlDynamicWidgetParser : IDynamicWidgetPropertyParser
{
    private readonly ISession _session;
    private readonly IGetCurrentPage _getCurrentPage;
    private readonly IGetCurrentUserCultureInfo _getCurrentCultureInfo;


    public PageUrlDynamicWidgetParser(ISession session, IGetCurrentPage getCurrentPage,
        IGetCurrentUserCultureInfo getCurrentCultureInfo)
    {
        _session = session;
        _getCurrentPage = getCurrentPage;
        _getCurrentCultureInfo = getCurrentCultureInfo;
    }

    public string Name => "pageUrl";

    public async Task<string> ParseAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        return existingValue;
    }
}