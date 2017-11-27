using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.Extensions.WebEncoders;

namespace Robonom.Common
{
    public class DefaultController : BaseController<DefaultController>
    {
        [ViewContext]
        public ViewContext ViewContext { get; set; }
        private ICompositeViewEngine _viewEngine;
        private IRazorViewEngine _razorViewEngine;
        private IActionContextAccessor _actionContextAccessor;
        private IViewComponentHelper _viewComponentHelper;
        public Page Page = new Page();

        //private ITempDataProvider _tempDataProvider;
        //private IHttpContextAccessor _httpContextAccessor;
        //RazorView razorView = null;
        //DefaultViewComponentHelper viewComponentHelper = null;

        public DefaultController(
            IViewComponentDescriptorCollectionProvider descriptorProvider,
            IViewComponentSelector selector,
            IViewComponentInvokerFactory invokerFactory,
            //IRazorPageFactory razorPageFactory,
            IRazorPageActivator pageActivator,
            //IRazorPage viewStartPage,
            ICompositeViewEngine compositeViewEngine,
            IRazorViewEngine razorViewEngine,
            IActionContextAccessor actionContextAccessor,
            IViewComponentHelper viewComponentHelper,
            IViewBufferScope viewBufferScope
            //IHtmlEncoder htmlEncoder
            )
        {
            //viewComponentHelper = new DefaultViewComponentHelper(descriptorProvider, HtmlEncoder.Default, selector, invokerFactory, viewBufferScope);
            //razorView = new RazorView(razorViewEngine, pageActivator,new ViewStart, viewStartPage, HtmlEncoder.Default,);

            _viewEngine = compositeViewEngine;
            _razorViewEngine = razorViewEngine;
            _actionContextAccessor = actionContextAccessor;
            _viewComponentHelper = viewComponentHelper;

            Page.HttpContext=_httpContext;
            Page.ActionContext=_actionContext;
            Page.Config=_config;
            Page.Environment=_hostingEnvironment;
        }

        /// <summary>
        /// All requests comes here
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            dynamic pageInfo = Site.GetPageInfo(this.HttpContext);
            Page.Info = pageInfo;

            bool isDevelopment = _hostingEnvironment.EnvironmentName.Equals("Development");
            string message = "";

            //Checking PageInfo
            if (pageInfo == null)
            {
                message = "<H1>404 page not found!</H1>";
                if (isDevelopment) message += "<hr>" + Request.Path;
                ViewData["Message"] = message;
                return View(Site.GetConfig("ErrorPage"));
            }


            //Checking Layout
            string realLayoutPath = Site.Environment.ContentRootPath + Site.LayoutsPath + pageInfo.layout;

            if (!System.IO.File.Exists(realLayoutPath))
            {
                message = "<H1>404 Layout file not found!</H1>";
                if (isDevelopment) message += "<hr>" + realLayoutPath;
                ViewData["Message"] = message;
                return View(Site.GetConfig("ErrorPage"));
            }

            string layoutPath = Site.LayoutsPath + pageInfo.layout;
            return View(layoutPath, Page);

            #region experimental
            //ViewData["HTML"] = System.IO.File.ReadAllText(realLayoutPath);


            //ViewEngineResult viewResult = tmp.FindView(controller.ActionContext, viewName);

            //ViewContext context = new ViewContext(ActionContext, null, ViewData, null, null);
            //helper.Contextualize(context);
            //string st1 = helper.Invoke("My", null).ToString();
            //return View();

            //string layoutHtml = System.IO.File.ReadAllText(realLayoutPath);



            //parse for sub loads

            //parse for skins as parallel

            //parse for apps as parallel


            //paralel tasklarin bitmesi beklendikten sonra result html response olarak verilecek

            //return Content(layoutHtml, "text/html"); //Bu yontemde Layout.cshtml ve _ViewImport.cshtml kullanilmiyor.
            //yada
            //HtmlString sonuc=helper.Invoke<Module1ViewComponent>(viewcontext);
            //sonuc.Wait();

            //ViewComponentResult res = Module1ViewComponent();
            //res.ExecuteResult(this.ActionContext);
            //string res1 = res.ToString();

            //VirtualPathProvider olamli, ilerde cshtml ler veritabanindan da gelebilir, su anda diskteki bir klasorden geliyor

            //ViewData["HTML"] = layoutHtml;


            //return Content(GetViewComponent("Module1").ToString());
            //return  View(); 
            #endregion
        }


        //public HtmlString GetViewComponent(string name)
        //{
        //    return  _viewComponentHelper.Invoke(name);
        //}

        public ActionResult GetViewComponent2(string name)
        {
            return ViewComponent(name);
        }

    }
 
}
