using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public interface IDynamicWidgetParser
{
    Task<IHtmlContent> ParseAsync(IHtmlHelper helper, string text, string props);
}