using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using MrCMS.Web.Apps.DynamicWidget.Entities;
using MrCMS.Web.Apps.DynamicWidget.Widgets;

namespace MrCMS.Web.Apps.DynamicWidget.DbConfiguration;

public class TemplateWidgetMappingOverride: IAutoMappingOverride<TemplateWidget>
{
    public void Override(AutoMapping<TemplateWidget> mapping)
    {
        mapping.References(f => f.HtmlTemplate, "HtmlTemplateId");
    }
}