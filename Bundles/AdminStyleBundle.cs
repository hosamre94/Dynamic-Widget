using System.Collections.Generic;
using System.Threading.Tasks;
using MrCMS.Website.Optimization;

namespace MrCMS.Web.Apps.DynamicWidget.Bundles;

public class AdminStyleBundle : IAdminStyleBundle
{
    public int Priority => int.MaxValue;

    public Task<bool> ShouldShow(string theme)
    {
        return Task.FromResult(true);
    }

    public string Url => "/Apps/DynamicWidgets/assets/dynamic-widget-admin.css";

    public IEnumerable<string> VendorFiles
    {
        get { yield return "/Apps/DynamicWidgets/Area/Admin/lib/aceeditor/css/ace.css"; }
    }
}