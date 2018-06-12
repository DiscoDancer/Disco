using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.TagHelpers
{
    public class PlaySoundTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public int SoundId { get; set; }

        public PlaySoundTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }

        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            var result = new TagBuilder("span");

            var playButton = CreatePlayButton();
            var playControls = CreatePlayControls();
            result.InnerHtml.AppendHtml(playButton);
            result.InnerHtml.AppendHtml(playControls);

            output.Content.AppendHtml(result.InnerHtml);
            output.Attributes.Add("class", "custom-audio-component");
            output.TagName = "span";
        }

        private TagBuilder CreatePlayControls()
        {
            var audio = new TagBuilder("audio");
            audio.Attributes["controls"] = "controls";

            var source = new TagBuilder("source");
            audio.Attributes["type"] = "audio/mpeg";
            audio.Attributes["src"] = AudioUrl;

            const string audioNotAvaliable = "Your browser does not support the audio element.";

            audio.InnerHtml.AppendHtml(source);
            audio.InnerHtml.Append(audioNotAvaliable);

            return audio;
        }

        private string AudioUrl
        {
            get
            {
                var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
                return urlHelper.Action("GetSoundById", "Timer",
                    new
                    {
                        id = SoundId
                    });
            }
        }

        private TagBuilder CreatePlayButton()
        {
            var btn = new TagBuilder("input");
            btn.Attributes["type"] = "button";
            btn.AddCssClass("play-icon");
            btn.Attributes["value"] = "|>";

            return btn;
        }
    }
}
