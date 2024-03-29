﻿using MrCMS.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class MediaUrlDynamicWidgetParser : IDynamicWidgetPropertyParser
{
    public string Name => "mediaUrl";

    public async Task<string> ParseAsync(IHtmlHelper helper, string name, string existingValue,
        AttributeItem[] attributes = null)
    {
        var width = 0;
        var height = 0;
        if (attributes != null)
            foreach (var attr in attributes)
            {
                switch (attr.Key)
                {
                    case "width":
                        int.TryParse(attr.Value, out width);
                        break;
                    case "height":
                        int.TryParse(attr.Value, out height);
                        break;
                }
            }

        var size = default(Size);
        if (width > 0)
            size = new Size { Width = width };

        if (height > 0)
            size.Height = height;

        return await helper.GetImageUrl(existingValue, size);
    }
}