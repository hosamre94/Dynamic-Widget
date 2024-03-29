﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class UniqueKeyDynamicWidgetParser : IDynamicWidgetPropertyParser
{
    public string Name => "uniqueKey";

    public Task<string> ParseAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        return Task.FromResult(existingValue);
    }
}