// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.Extensions.DependencyInjection;
// using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Services;
// using MrCMS.Web.Apps.DynamicWidget.Models;
//
// namespace MrCMS.Web.Apps.DynamicWidget.Core;
//
// public class HtmlTemplateDynamicWidgetParser : IDynamicWidgetPropertyParser
// {
//     private readonly IHtmlTemplateAdminService _htmlTemplateAdminService;
//     
//     public HtmlTemplateDynamicWidgetParser(IHtmlTemplateAdminService htmlTemplateAdminService)
//     {
//         _htmlTemplateAdminService = htmlTemplateAdminService;
//     }
//
//     public string Name => "htmlTemplate";
//
//     public async Task<string> ParseAsync(IHtmlHelper helper, string name, string existingValue,
//         AttributeItem[] attributes = null)
//     {
//         if (int.TryParse(existingValue, out var templateId))
//         {
//             var template = await _htmlTemplateAdminService.GetAsync(templateId);
//             return (await helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IDynamicWidgetParser>()
//                 .ParseAsync(helper, template.Text, props: null)).ToString();
//         }
//
//         return "";
//     }
// }