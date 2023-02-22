using AutoMapper;
using MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Models;
using MrCMS.Web.Apps.DynamicWidget.Entities;

namespace MrCMS.Web.Apps.DynamicWidget.Areas.Admin.Mapping;

public class HtmlTemplateMappingProfile : Profile
{
    public HtmlTemplateMappingProfile()
    {
        CreateMap<HtmlTemplate, AddHtmlTemplateModel>().ReverseMap();
        CreateMap<HtmlTemplate, UpdateHtmlTemplateModel>().ReverseMap();
    }
}