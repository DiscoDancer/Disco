using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication.TagHelpers
{
    public class ActivityEditDeleteTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public ActivityEditDeleteTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public int ActivityId { get; set; }

        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            var result = new TagBuilder("span");

            var editTag = CreateEditButton(urlHelper);
            var deleteTag = CreateDeleteButton(urlHelper);
            result.InnerHtml.AppendHtml(editTag);
            result.InnerHtml.AppendHtml(deleteTag);

            output.Content.AppendHtml(result.InnerHtml);
            output.TagName = "span";
        }

        private TagBuilder CreateEditButton(IUrlHelper urlHelper)
        {
            var editTag = new TagBuilder("a");
            editTag.Attributes["href"] = urlHelper.Action("EditActivity",
                new
                {
                    activityId = ActivityId
                });
            editTag.AddCssClass("anchor-no-visually-effects");

            var editTagSpan = new TagBuilder("span");
            editTagSpan.AddCssClass("edit-icon");
            editTagSpan.InnerHtml.Append("!");

            editTag.InnerHtml.AppendHtml(editTagSpan);

            return editTag;
        }

        private TagBuilder CreateDeleteButton(IUrlHelper urlHelper)
        {
            var deleteTag = new TagBuilder("a");
            deleteTag.Attributes["href"] = urlHelper.Action("DeleteActivity", ActivityId);
            deleteTag.AddCssClass("anchor-no-visually-effects");

            var deleteTagSpan = new TagBuilder("span");
            deleteTagSpan.AddCssClass("delete-icon");
            deleteTagSpan.InnerHtml.Append("X");

            deleteTag.InnerHtml.AppendHtml(deleteTagSpan);

            return deleteTag;
        }
    }
}
