using System.Threading.Tasks;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models;
using X.PagedList;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;

public interface IWebpageUrlSelectorService
{
    Task<IPagedList<UrlSelectorLookupResult>> Search(string term, int page);
}