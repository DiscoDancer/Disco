using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication.TagHelpers
{
    public abstract class EditDeleteTagHelper : TagHelper
    {
        public abstract string EditUrl { get; }
        public abstract string DeleteUrl { get; }

        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            var result = new TagBuilder("span");

            var editTag = CreateEditButton(EditUrl);
            var deleteTag = CreateDeleteButton(DeleteUrl);
            result.InnerHtml.AppendHtml(editTag);
            result.InnerHtml.AppendHtml(deleteTag);

            output.Content.AppendHtml(result.InnerHtml);
            output.TagName = "span";
        }

        private TagBuilder CreateEditButton(string EditUrl)
        {
            var editTag = new TagBuilder("a");
            editTag.Attributes["href"] = EditUrl;
            editTag.AddCssClass("anchor-no-visually-effects");

            var editTagSpan = new TagBuilder("span");
            editTagSpan.AddCssClass("edit-icon");
            editTagSpan.InnerHtml.Append("!");

            editTag.InnerHtml.AppendHtml(editTagSpan);

            return editTag;
        }

        private TagBuilder CreateDeleteButton(string DeleteUrl)
        {
            var deleteTag = new TagBuilder("a");
            deleteTag.Attributes["href"] = DeleteUrl;
            deleteTag.AddCssClass("anchor-no-visually-effects");

            var deleteTagSpan = new TagBuilder("span");
            deleteTagSpan.AddCssClass("delete-icon");
            deleteTagSpan.InnerHtml.Append("X");

            deleteTag.InnerHtml.AppendHtml(deleteTagSpan);

            return deleteTag;
        }
    }
}
