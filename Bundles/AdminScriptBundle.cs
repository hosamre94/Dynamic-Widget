using System.Collections.Generic;
using System.Threading.Tasks;
using MrCMS.Website.Optimization;

namespace MrCMS.Web.Apps.DynamicWidget.Bundles;

public class AdminScriptBundle: IAdminScriptBundle
{
    public int Priority => int.MaxValue;

    public Task<bool> ShouldShow(string theme)
    {
        return Task.FromResult(true);
    }

    public string Url => "/Apps/DynamicWidgets/assets/dynamic-widget-admin.js";

    public IEnumerable<string> VendorFiles
    {
        get
        {
            yield return "/Apps/DynamicWidgets/Content/libs/jquery.serializejson.js";
            yield return "/Apps/DynamicWidgets/Area/Admin/lib/aceeditor/js/ace.js";
        }
    }
}