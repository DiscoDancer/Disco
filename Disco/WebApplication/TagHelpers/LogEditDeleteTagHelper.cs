using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication.TagHelpers
{
    public class LogEditDeleteTagHelper: EditDeleteTagHelper
    {
        public override string EditUrl => string.Empty;

        public override string DeleteUrl
        {
            get
            {
                var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                return urlHelper.Action("DeleteLog", "Timer",
                    new
                    {
                        id = LogId
                    });
            }
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public int LogId { get; set; }


        private readonly IUrlHelperFactory _urlHelperFactory;

        public LogEditDeleteTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }
    }
}
