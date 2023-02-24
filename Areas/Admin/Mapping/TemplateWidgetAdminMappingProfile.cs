using AutoMapper;
using MrCMS.Web.Admin.Infrastructure.Mapping;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models.Widgets;
using MrCMS.Web.Apps.DynamicWidget.Widgets;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Mapping;

public class TemplateWidgetAdminMappingProfile : Profile
{
    public TemplateWidgetAdminMappingProfile()
    {
        CreateMap<TemplateWidget, TemplateWidgetAddModel>()
            .ForMember(f => f.TemplateId, f => f.MapFrom(x => x.HtmlTemplate.Id))
            .ReverseMap()
            .MapEntityLookup(f => f.TemplateId, f => f.HtmlTemplate);

        CreateMap<TemplateWidget, TemplateWidgetUpdateModel>()
            .ForMember(f => f.Template, f => f.MapFrom(x => x.HtmlTemplate))
            .ReverseMap()
            .ForMember(f => f.HtmlTemplate, f => f.Ignore());
    }
}