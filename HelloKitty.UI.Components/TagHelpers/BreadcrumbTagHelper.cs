using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HelloKitty.UI.Components.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("breadcrumb")]
    public class BreadcrumbTagHelper : TagHelper
    {
        private readonly IHtmlGenerator generator;

        public BreadcrumbTagHelper(IHtmlGenerator generator)
        {
            this.generator = generator;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            /*
             * <nav aria-label="breadcrumb">
    <ol class="breadcrumb">
        @foreach (var parent in parents)
        {
            path = PathHelper.ConvertFileSystemPathToPath(parent.GetRelativePath());
            <li class="breadcrumb-item"><a asp-action="Index" asp-route-path="@path">@parent.Name</a></li>
        }
        <li class="breadcrumb-item active" aria-current="page">@currentFolderName</li>
    </ol>
</nav>
             */
            output.TagName = "nav";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add(new TagHelperAttribute("aria-label", "breadcrumb"));
            var innerHtml = new HtmlContentBuilder();
            innerHtml.AppendHtml("<ol class=\"breadcrumb\">");
            var target = await output.GetChildContentAsync();
            innerHtml.AppendHtml(target);
            innerHtml.AppendHtml("</ol>");
            output.Content.SetHtmlContent(innerHtml);
        }
    }
}
