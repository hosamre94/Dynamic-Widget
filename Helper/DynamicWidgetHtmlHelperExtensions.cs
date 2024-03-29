﻿using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using MrCMS.Web.Apps.DynamicWidget.Core;

namespace MrCMS.Web.Apps.DynamicWidget.Helper;

public static class DynamicWidgetHtmlHelperExtensions
{
    public static async Task<IHtmlContent> RenderDynamicContent<T>(this IHtmlHelper<T> helper,
        Expression<Func<T, string>> textMethod, Expression<Func<T, string>> propertiesMethod)
    {
        var model = helper.ViewData.Model;
        if (model == null)
            return HtmlString.Empty;

        var text = textMethod?.Compile().Invoke(model);
        var props = propertiesMethod?.Compile().Invoke(model);

        return await helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IDynamicWidgetParser>()
            .ParseAsync(helper, text, props);
    }


    public static async Task<IHtmlContent> RenderAdminProperties<T>(this IHtmlHelper<T> helper,
        Expression<Func<T, string>> textMethod, Expression<Func<T, string>> propertiesMethod)
    {
        var model = helper.ViewData.Model;
        if (model == null)
            return HtmlString.Empty;

        var text = textMethod?.Compile().Invoke(model);
        var json = propertiesMethod?.Compile().Invoke(model);
        var props = JsonSerializer.Deserialize<JsonElement>(json ?? "null");

        return await helper.ViewContext.HttpContext.RequestServices.GetRequiredService<IDynamicWidgetRender>()
            .RenderAsync(helper, text, props);
    }
}