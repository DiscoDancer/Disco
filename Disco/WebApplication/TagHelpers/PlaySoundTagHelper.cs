using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication.TagHelpers
{
    public class PlaySoundTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            var result = new TagBuilder("span");

            var playTag = CreatePlayButton();
            result.InnerHtml.AppendHtml(playTag);

            output.Content.AppendHtml(result.InnerHtml);
            output.TagName = "span";
        }

        private TagBuilder CreatePlayButton()
        {
            var editTag = new TagBuilder("a");
            editTag.Attributes["href"] = "google.com";
            editTag.AddCssClass("anchor-no-visually-effects");

            var editTagSpan = new TagBuilder("span");
            editTagSpan.AddCssClass("play-icon");
            editTagSpan.InnerHtml.Append("|>");

            editTag.InnerHtml.AppendHtml(editTagSpan);

            return editTag;
        }
    }
}
