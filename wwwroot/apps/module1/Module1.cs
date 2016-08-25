using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ViewComponents;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Robonom.Web.Core.Apps.Common
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
        }       // GET: /<controller>/
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
            }


            /*
             * qr icinde gelen method kendisine ait ise calistiracak
             * method=HttpContext.Request.QueryString.split("qr")[1].substring(indexof("/))
             * if(method!=null && method=="haberKaydet"){
             *    await haberKaydet();
             * }
             *             
             */



          
            return View(baseUrl);
        }


        public  void haberKaydet()
        {
            //...           
        }
    }
}
