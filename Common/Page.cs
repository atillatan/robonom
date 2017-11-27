using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace Robonom.Common
{
    public class Page //: PageModel
    {
        public dynamic Info;
        public Page(){

        }

        public HttpContext HttpContext { get; internal set; }
        public ActionExecutingContext ActionContext { get; internal set; }
        public IConfiguration Config { get; internal set; }
        public IHostingEnvironment Environment { get; internal set; }
    }
}

