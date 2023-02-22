// using System.Threading.Tasks;
// using MrCMS.Entities.Documents.Web;
// using MrCMS.Services;
// using Microsoft.AspNetCore.Html;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using MrCMS.Services.Resources;
// using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;
// using MrCMS.Web.Apps.DynamicWidget.Models;
// using NHibernate;
// using NHibernate.Linq;
//
// namespace MrCMS.Web.Apps.DynamicWidget.Core;
//
// public class HtmlTemplateDynamicWidgetRender : IDynamicWidgetPropertyRender
// {
//     private readonly IGetHtmlTemplateOptions _getHtmlTemplateOptions;
//     private readonly IStringResourceProvider _stringResourceProvider;
//
//
//     public HtmlTemplateDynamicWidgetRender(IGetHtmlTemplateOptions getHtmlTemplateOptions, IStringResourceProvider stringResourceProvider)
//     {
//         _getHtmlTemplateOptions = getHtmlTemplateOptions;
//         _stringResourceProvider = stringResourceProvider;
//     }
//
//     public string Name => "htmlTemplate";
//
//     public async Task<IHtmlContent> RenderAsync(IHtmlHelper helper, string name, string existingValue,
//         AttributeItem[] attributes = null)
//     {
//         var tagBuilder = new TagBuilder("select")
//         {
//             Attributes =
//             {
//                 ["id"] = TagBuilder.CreateSanitizedId(name, "-"),
//                 ["name"] = name,
//                 ["data-dynamic-input"] = null
//             }
//         };
//
//         var options = await _getHtmlTemplateOptions.GetOptions(f => Task.FromResult(f.Id.ToString() == existingValue), await _stringResourceProvider.GetValue("Select a template..."));
//
//         foreach (var selectItem in options)
//         {
//             tagBuilder.InnerHtml.AppendHtml($"<option value='{selectItem.Value}' {(selectItem.Selected ? "selected" : "")}>{selectItem.Text}</option>");
//         }
//         
//         //TODO: Should render each template html after dropdown change
//
//         tagBuilder.AddCssClass("form-control");
//
//         return await Task.FromResult(tagBuilder);
//     }
// }