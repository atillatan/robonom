using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

using Robonom.Common;

namespace Robonom.Apps
{
    public class Seo : ViewComponent
    {
        private IViewComponentHelper _viewComponentHelper;

        public Seo(IViewComponentHelper viewComponentHelper)
        {
            _viewComponentHelper = viewComponentHelper;
            Console.WriteLine(_viewComponentHelper.ToString());
        }

        public async Task<IViewComponentResult> InvokeAsync(string url)
        {
            string viewPath = $"{"~"}{Site.AppsPath}{"/seo/Index.cshtml"}";
            string staticHtml = (Site.WebRootPath + url);
            return View(viewPath, await File.ReadAllTextAsync(staticHtml));
        }
    }
}
