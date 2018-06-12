using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication.TagHelpers
{
    public class ActivityEditDeleteTagHelper : EditDeleteTagHelper
    {
        public override string EditUrl
        {
            get
            {
                var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                return urlHelper.Action("EditActivity", "Timer",
                        new
                        {
                            activityId = ActivityId
                        });
            }
        }

        public override string DeleteUrl
        {
            get
            {
                var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                return urlHelper.Action("DeleteActivity", "Timer",
                    new
                    {
                        activityId = ActivityId
                    });
            }
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public int ActivityId { get; set; }


        private readonly IUrlHelperFactory _urlHelperFactory;

        public ActivityEditDeleteTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }

    }
}
