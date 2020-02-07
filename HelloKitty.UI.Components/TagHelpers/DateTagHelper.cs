using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloKitty.UI.Components.TagHelpers
{
    public class DateTagHelper : TagHelper
    {
        public bool ShowUtc { get; set; } = true;
        public DateTime? Date { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var dt = Date ?? DateTime.Now;
            dt = TimeTagHelper.ConvertTo(dt, ShowUtc);
            output.TagName = "";
            output.Content.SetContent($"Current {(ShowUtc ? "UTC " : "")}date: {dt.ToString("dd/mm/yyyy")}");
            output.PreContent.AppendHtml("<div>");
            output.PostContent.AppendHtml("</div>");
        }
    }
}
