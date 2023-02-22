using System.Threading.Tasks;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models;
using MrCMS.Web.Apps.DynamicWidget.Entities;
using X.PagedList;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;

public interface IHtmlTemplateAdminService
{
    Task<IPagedList<HtmlTemplate>> SearchAsync(HtmlTemplateSearchModel searchModel);
    Task AddAsync(AddHtmlTemplateModel addHtmlTemplateModel);
    Task<HtmlTemplate> GetAsync(int id);
    Task<bool> IsUniqueName(string name, int? id);
    Task<UpdateHtmlTemplateModel> GetUpdateModel(int id);
    Task UpdateAsync(UpdateHtmlTemplateModel model);
    Task DeleteAsync(int id);
}