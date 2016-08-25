using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Razor;

namespace Robonom.Web.Core.apps.common
{

    [HtmlTargetElement("skin", Attributes = SkinPath)]
    public class RobonomTagHelper : TagHelper
    {
        private const string SkinPath = "asp-src";

        [HtmlAttributeName(SkinPath)]
        public string Path { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //output.Attributes["src"] ="falan filan";
        }
    }
}
