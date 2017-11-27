using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewComponents;

 
namespace Robonom.Apps
{
    [ViewComponent(Name = "Module1")]
    public class Module1 : ViewComponent
    {
        private IViewComponentHelper _viewComponentHelper;

        private readonly IHostingEnvironment _environment;

        public Module1(IHostingEnvironment environment, IViewComponentHelper viewComponentHelper)
        {
            _environment = environment;
            _viewComponentHelper = viewComponentHelper;
            Console.WriteLine(_viewComponentHelper.ToString());
            Console.WriteLine(_environment.ToString());
        }
        public ViewViewComponentResult Invoke(string url)//Asenkron yapilmali
        {
            string baseUrl = "";
            string[] parameters;

            if (url.Contains("?"))
            {
                string[] urlparts = url.Split('?');
                baseUrl = urlparts[0];
                string paramStr = urlparts[1];
                if (paramStr.Contains("&")) parameters = paramStr.Split('&');
                else parameters = new string[] { paramStr };
                return View(baseUrl, model: parameters);

            }
            else
            {
                return View(baseUrl);
            }
        }
    }
}
