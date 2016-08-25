using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Http;

namespace Robonom.Web.Core.Apps.Common
{
    public class BaseController<T> : Controller
    {
        protected HttpClient _httpClient = HttpConnectionClient.Current.Client;
        public static ActionExecutingContext _actionContext = null;
        protected IConfiguration _config;
        protected IServiceProvider _aspNetServiceProvider { get; set; }
        //protected IApplicationEnvironment _applicationEnvironment { get; set; }//net46
        protected IHostingEnvironment _hostingEnvironment { get; set; }
        protected HttpContext _httpContext { get; set; }
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            _actionContext = actionContext;
            _aspNetServiceProvider = actionContext.HttpContext.RequestServices;
            _config = (IConfiguration)_aspNetServiceProvider.GetService(typeof(IConfiguration));
            //_applicationEnvironment = (IApplicationEnvironment)_aspNetServiceProvider.GetService(typeof(IApplicationEnvironment));net46
            _hostingEnvironment = (IHostingEnvironment)_aspNetServiceProvider.GetService(typeof(IHostingEnvironment));
            _httpContext = actionContext.HttpContext;
            //ViewData["_HttpContext"] = _HttpContext;atilla:performanstan dolayi iptal edildi
            base.OnActionExecuting(actionContext);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            buildResponseHeader();
            base.OnActionExecuted(context);
        }
        private void buildResponseHeader()
        {
            /*
             * Atilla:
             * Development ortaminda tarayicinin cache tutmamasi icin eklendi, diger bir ayar internet explorer dan yapilabilir 
             * menu>internet options>general>settings>everytime i visit the webpage
             * menu>internet secenekleri>genel>ayarlar>sayfayi her ziyaret ettigimde
            */
            if (_hostingEnvironment.EnvironmentName.Equals("Development"))
            {
                IHeaderDictionary headers = HttpContext.Response.Headers;
                headers.Add("Cache-Control", "no-cache");
                headers.Add("Pragma", "no-cache");
                headers.Add("Access-Control-Max-Age", "-1");
            }
        }
    }
}
