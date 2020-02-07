using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace HelloKitty.UI.Components.TagHelpers
{
    [HtmlTargetElement("item", ParentTag = "breadcrumb")]
    public class BreadcrumbItemTagHelper : AnchorTagHelper
    {
        public BreadcrumbItemTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public bool Active { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            /*
             * <li class="breadcrumb-item"><a asp-action="Index" asp-route-path="@path">@parent.Name</a></li>
             */
            output.TagName = "li";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.AddClass("breadcrumb-item", HtmlEncoder.Default);
            if (Active)
            {

                output.AddClass("active", HtmlEncoder.Default);
                output.Content.SetHtmlContent(await output.GetChildContentAsync());
            }
            else
            {
                var anchorOutput = new TagHelperOutput("a", new TagHelperAttributeList(), (useCachedResult, encoder) => Task.Factory.StartNew<TagHelperContent>(() => new DefaultTagHelperContent()));
                await base.ProcessAsync(context, anchorOutput);
                anchorOutput.Content.SetHtmlContent(await output.GetChildContentAsync());
                output.Content.SetHtmlContent(anchorOutput);
            }
        }
    }
}