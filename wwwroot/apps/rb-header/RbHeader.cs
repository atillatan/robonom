using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using NuGet.Common;
using Robonom.Web.Core.Apps.Common;

namespace Robonom.Web.Core.wwwroot.apps.rb_header
{
    public class RbHeader : ViewComponent
    {
        private IViewComponentHelper _viewComponentHelper;

        public RbHeader(IViewComponentHelper viewComponentHelper)
        {
           _viewComponentHelper = viewComponentHelper;
            Console.WriteLine(_viewComponentHelper.ToString());
        }

        public async Task<IViewComponentResult> InvokeAsync(string url)
        {
            string viewPath = $"{"~"}{Current.AppsPath}{"/rb-header/Index.cshtml"}";
            string templateHtml = (Current.RootPath(this.HttpContext) + url);
            await Task.Delay(100);
            return View(viewPath, templateHtml);
        }

        //asenkron yapilamli
        //public  ViewViewComponentResult Invoke(string url)
        //{
        //    string viewPath = $"{"~"}{Current.AppsPath}{"/rb-header/Index.cshtml"}";
        //    string templateHtml = File.ReadAllText(Current.RootPath(this.HttpContext) + url);
        //    return  View(viewPath, templateHtml);
        //}
    }
}
