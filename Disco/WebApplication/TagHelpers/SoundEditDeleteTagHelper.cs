using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebApplication.TagHelpers
{
    public class SoundEditDeleteTagHelper : EditDeleteTagHelper
    {
        public override string EditUrl
        {
            get
            {
                var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                return urlHelper.Action("EditSound",
                    new
                    {
                        soundId = SoundId
                    });
            }
        }

        public override string DeleteUrl
        {
            get
            {
                var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                return urlHelper.Action("DeleteSound",
                    new
                    {
                        soundId = SoundId
                    });
            }
        }

        [ViewContext] [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public int SoundId { get; set; }


        private readonly IUrlHelperFactory _urlHelperFactory;

        public SoundEditDeleteTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }
    }
}
