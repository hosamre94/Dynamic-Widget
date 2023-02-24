using System.Threading.Tasks;
using MrCMS.Web.Apps.DynamicWidget.Entities;

namespace MrCMS.Web.Apps.DynamicWidget.Services;

public interface IGetHtmlTemplateService
{
    Task<HtmlTemplate> Get(int id);
}