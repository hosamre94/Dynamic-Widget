using MrCMS.Helpers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentNHibernate.Conventions.Inspections;
using MrCMS.Shortcodes.Forms;
using MrCMS.Web.Apps.DynamicWidget.Helper;
using MrCMS.Web.Apps.DynamicWidget.Models;

namespace MrCMS.Web.Apps.DynamicWidget.Core;

public class DynamicWidgetRender : IDynamicWidgetRender
{
    private static readonly Regex ShortcodeMatcher =
        new(@"\[([\w-_]+)([^\]]*)?\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private readonly Dictionary<string, IDynamicWidgetPropertyRender> _renderers;

    private readonly string _checkboxPrefix = "CBI_";

    public DynamicWidgetRender(IEnumerable<IDynamicWidgetPropertyRender> renderers)
    {
        _renderers = renderers.ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
    }

    public bool CanRender(string typeName)
    {
        return _renderers.ContainsKey(typeName);
    }

    private bool IsArray(string typeName)
    {
        return string.Equals(typeName, "array", StringComparison.OrdinalIgnoreCase) ||
               string.Equals(typeName, "array-end", StringComparison.OrdinalIgnoreCase);
    }

    public async Task<IHtmlContent> RenderAsync(IHtmlHelper helper, string text, JsonElement properties)
    {
        if (string.IsNullOrEmpty(text))
            return HtmlString.Empty;

        return await RenderProperties(helper, text, properties);
    }

    private async Task<IHtmlContentBuilder> RenderProperties(IHtmlHelper helper, string text, JsonElement properties)
    {
        var htmlContent = new HtmlContentBuilder();
        var matchCollection = ShortcodeMatcher.Matches(text);
        if (!matchCollection.Any())
            return htmlContent;

        var treeNodeList = BuildPropertiesTree(matchCollection);

        var  container = new TagBuilder("div");
        container.AddCssClass("dynamic-widget-container");
        
        var row = new TagBuilder("div");
        row.AddCssClass("row");

        //TODO
        //check if it can run in parallel
        foreach (var node in treeNodeList)
            row.InnerHtml.AppendHtml(await RenderProperties(helper, node, properties));

        container.InnerHtml.AppendHtml(row);

        htmlContent.AppendHtml(container);
        
        return htmlContent;
    }

    private async Task<IHtmlContent> RenderProperties(IHtmlHelper helper, RenderNode node, JsonElement properties,
        string prefix = null)
    {
        return node.Children != null && node.Children.Any()
            ? await RenderArray(helper, node, properties, prefix)
            : await RenderProperty(helper, node, properties, prefix);
    }

    #region Tree

    private List<RenderNode> BuildPropertiesTree(MatchCollection matchCollection)
    {
        var tree = new List<RenderNode>();
        for (var i = 0; i < matchCollection.Count; i++)
        {
            var match = matchCollection[i];
            if (match.Groups.Count != 3)
                continue;

            var attributes = AttributesHelper.GetAttributes(match.Groups[2].Value);
            if (attributes.Length == 0)
                continue;

            var nameValueTuple = attributes.FirstOrDefault(x => x.Key == "name");
            if (string.IsNullOrEmpty(nameValueTuple.Value))
                continue;

            var typeName = match.Groups[1].Value;
            var node = new RenderNode(nameValueTuple.Value, typeName, attributes);
            if (string.Equals(typeName, "array", StringComparison.OrdinalIgnoreCase))
                node.Children = GetChildren(matchCollection, ref i, typeName);

            if (tree.All(x => x.Name != nameValueTuple.Value))
                tree.Add(node);
        }

        return tree;
    }

    private List<RenderNode> GetChildren(MatchCollection matchCollection, ref int i, string parentName)
    {
        var children = new List<RenderNode>();
        for (i += 1; i < matchCollection.Count; i++)
        {
            var match = matchCollection[i];
            if (match.Groups.Count != 3)
                continue;

            var typeName = match.Groups[1].Value;
            if (string.Equals(typeName, "array-end", StringComparison.OrdinalIgnoreCase))
                return children;

            var attributes = AttributesHelper.GetAttributes(match.Groups[2].Value);
            if (attributes.Length == 0)
                continue;

            var nameValueTuple = attributes.FirstOrDefault(x => x.Key == "name");
            if (string.IsNullOrEmpty(nameValueTuple.Value))
                continue;

            var node = new RenderNode(nameValueTuple.Value, typeName, attributes);
            if (string.Equals(typeName, "array", StringComparison.OrdinalIgnoreCase))
                node.Children = GetChildren(matchCollection, ref i, nameValueTuple.Value);

            if (children.All(x => x.Name != nameValueTuple.Value))
                children.Add(node);
        }

        throw new Exception($"array '{parentName}' dose not have a closing tag");
    }

    #endregion

    #region List Render

    private async Task<IHtmlContent> RenderArray(IHtmlHelper helper, RenderNode node, JsonElement properties,
        string prefix = null)
    {
        var name = node.Name;
        var props = new JsonElement();
        if (properties.ValueKind != JsonValueKind.Null && properties.ValueKind != JsonValueKind.Undefined)
            properties.TryGetProperty(name, out props);


        //Card responsive can change from here
        var responsive = new TagBuilder("div");
        responsive.AddCssClass("col-12");
        
        //Table
        var container = new TagBuilder("div");
        container.AddCssClass("dynamic-widget-array-container");

        //Add headers
        var header = GetContainerHeader(name.BreakUpString());
        container.InnerHtml.AppendHtml(header);

        //Add rows
        prefix = $"{prefix}[{name}]";
        var rows = await GetDataCards(node.Children, props, helper, prefix);
        container.InnerHtml.AppendHtml(rows);


        var addButton = GetContainerAddButton(name.BreakUpString());
        container.InnerHtml.AppendHtml(addButton);

        responsive.InnerHtml.AppendHtml(container);
        
        return responsive;
    }

    #endregion

    #region Property Render

    private async Task<IHtmlContent> RenderProperty(IHtmlHelper helper, RenderNode node, JsonElement properties,
        string prefix)
    {
        var typeName = node.TypeName;
        var label = node.Name;

        if (!CanRender(typeName) || IsArray(typeName))
            return null;

        var existingProperty = properties.GetNullableProperty(label);
        var existingValue = existingProperty?.GetString();

        var isArray = prefix != null;
        
        prefix = prefix == null ? label : $"{prefix}[{label}]";

        var elementContainer = GetPropertyContainer();
        var elementHtml = await GetProperty(helper, typeName, label, prefix, node.Attributes, existingValue);

        if (!isArray) //instead of using show label
        {
            var enabledValue = existingProperty.HasValue
                ? properties.GetProperty($"{_checkboxPrefix}{prefix}").GetString()
                : "true";
            var enabledHtml = GetEnabledProperty(prefix, enabledValue);
            elementContainer.InnerHtml.AppendHtml(enabledHtml);
        }


        elementContainer.InnerHtml.AppendHtml(elementHtml);
        
        var responsive = new TagBuilder("div");
        responsive.AddCssClass(_renderers[typeName].ResponsiveClass);

        responsive.InnerHtml.AppendHtml(elementContainer);
        
        return responsive;
    }

    private async Task<IHtmlContent> GetProperty(IHtmlHelper helper, string typeName, string label, string name,
        AttributeItem[] attributes, string existingValue)
    {
        IHtmlContentBuilder elementHtml = new HtmlContentBuilder();

        elementHtml.AppendHtml(GetLabel(name, label));

        var render = await _renderers[typeName].RenderAsync(helper, name, existingValue, attributes);
        elementHtml.AppendHtml(render);

        return elementHtml;
    }

    private IHtmlContent GetLabel(string name, string label)
    {
        var tagBuilder = new TagBuilder("label");
        var elementId = TagBuilder.CreateSanitizedId(name, "-");
        label = label.Split('(')[0];

        tagBuilder.Attributes["for"] = elementId;

        tagBuilder.InnerHtml.Append(label.BreakUpString());

        return tagBuilder;
    }

    private TagBuilder GetPropertyContainer()
    {
        var elementContainer = new TagBuilder("div");
        elementContainer.AddCssClass("form-group");
        return elementContainer;
    }

    private IHtmlContent GetEnabledProperty(string name, string existingValue, bool addMargin = true,
        bool isForList = false)
    {
        var tagBuilder = new TagBuilder("input");
        if (addMargin)
            tagBuilder.AddCssClass("mr-2");
        tagBuilder.Attributes["type"] = "checkbox";
        tagBuilder.Attributes["name"] = isForList ? $"{name}[][{_checkboxPrefix}]" : $"{_checkboxPrefix}{name}";
        tagBuilder.Attributes["value"] = "true";
        if (existingValue == "true")
            tagBuilder.Attributes["checked"] = "checked";

        tagBuilder.Attributes["data-dynamic-input"] = null;

        return tagBuilder;
    }

    #endregion

    #region TableHeader

    private IHtmlContent GetContainerHeader(string title)
    {
        var header = new TagBuilder("h4");

        header.AddCssClass("border-bottom pb-3 my-3");

        header.InnerHtml.AppendHtml(title);

        return header;
    }

    private IHtmlContent GetContainerAddButton(string name)
    {
        var addButton = new TagBuilder("div");
        addButton.AddCssClass("row my-3 justify-content-center");

        addButton.InnerHtml.AppendHtml(
            $"<div class='col-auto'><button data-array-id='{Guid.NewGuid()}' type='button' class='btn btn-primary add-row'><i class='fa fa-plus'></i> Add item to {name}</button></div>");

        return addButton;
    }

    #endregion

    #region TableRows

    private async Task<IHtmlContentBuilder> GetDataCards(List<RenderNode> nodes, JsonElement props,
        IHtmlHelper helper,
        string prefix = null)
    {
        IHtmlContentBuilder bodyHtml = new HtmlContentBuilder();

        var i = 0;
        var existingPropsCount =
            props.ValueKind == JsonValueKind.Undefined ? 0 :
            props.ValueKind == JsonValueKind.Array ? props.EnumerateArray().Count() :
            props.EnumerateObject().Count();

        //always add at least a row
        do
        {
            var card = new TagBuilder("div");
            var nodeProps = props.ValueKind == JsonValueKind.Undefined ? new JsonElement() : props[i];
            var enabledValue = nodeProps.GetNullableProperty(_checkboxPrefix)?.GetString() ?? "true";

            card.AddCssClass("card mb-3");

            card.InnerHtml.AppendHtml(GetCardHeader(prefix, enabledValue, i));

            var cardBody = new TagBuilder("div");
            cardBody.AddCssClass("card-body py-2");
            
            var row = new TagBuilder("div");
            row.AddCssClass("row");
            
            foreach (var node in nodes)
            {
                var prefixName = $"{prefix}[]";

                var render = await RenderProperties(helper, node, nodeProps, prefixName);
                if (render != null)
                    row.InnerHtml.AppendHtml(render);
            }

            cardBody.InnerHtml.AppendHtml(row);
            card.InnerHtml.AppendHtml(cardBody);

            bodyHtml.AppendHtml(card);
            i++;
        } while (i < existingPropsCount);


        return bodyHtml;
    }

    private IHtmlContent GetCardHeader(string prefix, string existingValue, int index)
    {
        var header = new TagBuilder("div");
        header.AddCssClass("card-header bg-light py-2");

        header.InnerHtml.AppendHtml(
            $"<div class='row'><div class='col-auto p-0'>{GetEnabledProperty(prefix, existingValue, false, true).GetString()}</div><div class='col rowIndex font-weight-bold'>{(index + 1)}</div><div class='col-auto'><button type='button' class='btn btn-sm btn-danger delete-row'><i class='fa fa-trash-o'></i></button></div></div>");
            // <div class='col-auto'><button type='button' class='btn btn-sm sort-row'><i class='fa fa-sort'></i></button></div>
        
        return header;
    }

    #endregion
}