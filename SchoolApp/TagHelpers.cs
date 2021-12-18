using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SchoolApp
{
    [HtmlTargetElement("script")]
    public class ScriptTagHelpers : ScriptTagHelper
    {
        private const string appendVersion = "asp-append-version";

        public ScriptTagHelpers(
            IWebHostEnvironment hostingEnvironment,
            TagHelperMemoryCacheProvider cacheProvider,
            IFileVersionProvider fileVersionProvider,
            HtmlEncoder htmlEncoder,
            JavaScriptEncoder javaScriptEncoder,
            IUrlHelperFactory urlHelperFactory) : base(hostingEnvironment, cacheProvider, fileVersionProvider, htmlEncoder, javaScriptEncoder, urlHelperFactory)
        {

        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrWhiteSpace(base.Src) &&
                !(base.AppendVersion ?? false) &&
                !WaWUrlHelper.IsAbsoluteURL(base.Src))
            {
                var attributes = new TagHelperAttributeList(context.AllAttributes)
                {
                    { appendVersion, true }
                };

                context = new TagHelperContext(attributes, context.Items, context.UniqueId);

                base.AppendVersion = true;
            }

            return base.ProcessAsync(context, output);
        }
    }

    [HtmlTargetElement("link")]
    public class StyleTagHelpers : LinkTagHelper
    {
        private const string appendVersion = "asp-append-version";
        private const string rel = "rel";
        private const string stylesheet = "stylesheet";

        public StyleTagHelpers(
            IWebHostEnvironment hostingEnvironment,
            TagHelperMemoryCacheProvider cacheProvider,
            IFileVersionProvider fileVersionProvider,
            HtmlEncoder htmlEncoder,
            JavaScriptEncoder javaScriptEncoder,
            IUrlHelperFactory urlHelperFactory) : base(hostingEnvironment, cacheProvider, fileVersionProvider, htmlEncoder, javaScriptEncoder, urlHelperFactory)
        {

        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var relAttribute = context.AllAttributes.FirstOrDefault(f => f.Name.Equals(rel, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(base.Href) &&
                relAttribute != null &&
                relAttribute.Value.ToString().Equals(stylesheet, StringComparison.OrdinalIgnoreCase) &&
                !(base.AppendVersion ?? false) &&
                !WaWUrlHelper.IsAbsoluteURL(base.Href))
            {
                var attributes = new TagHelperAttributeList(context.AllAttributes)
                {
                    { appendVersion, true }
                };

                context = new TagHelperContext(attributes, context.Items, context.UniqueId);

                base.AppendVersion = true;
            }

            return base.ProcessAsync(context, output);
        }
    }
}
