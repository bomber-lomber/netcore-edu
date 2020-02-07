using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HelloKitty.UI.Components.TagHelpers
{
    public class TimeTagHelper : TagHelper
    {
        public bool ShowUtc { get; set; } = true;
        public DateTime? Date { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var dt = Date ?? DateTime.Now;
            dt = ConvertTo(dt, ShowUtc);
            output.TagName = "div";
            output.Content.SetContent($"Current {(ShowUtc ? "UTC " : "")}time: {dt.ToString("HH:mm:ss")}");
        }

        internal static DateTime ConvertTo(DateTime dt, bool convertToUtc)
        {
            if (convertToUtc)
            {
                if (dt.Kind != DateTimeKind.Utc)
                {
                    dt = dt.ToUniversalTime();
                }
            }
            else
            {
                if (dt.Kind == DateTimeKind.Utc)
                {
                    dt = dt.ToLocalTime();
                }
            }
            return dt;
        }
    }
}
