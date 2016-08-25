using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Robonom.Web.Core.Apps.Common
{
    public class Module2 : ViewComponent
    {
        private IViewComponentHelper _viewComponentHelper;

        public Module2(IViewComponentHelper viewComponentHelper)
        {
            _viewComponentHelper = viewComponentHelper;
            Console.WriteLine(_viewComponentHelper.ToString());
        }

        // GET: /<controller>/
        public  ViewViewComponentResult Invoke(string url)
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


            //var httpContext = Current.Htt
            //if (httpContext.Request.Method == "POST")
            //{
            //    return Post(BindModel<ContactFormViewModel>(), model);
            //}

            //return Get(new ContactFormViewModel { Page = model });

            string filePath = this.ViewContext.ExecutingFilePath;

           

            return View(baseUrl);
        }


        //private IViewComponentResult Get(ContactFormViewModel model)
        //{
        //    return View("Get", model);
        //}

        //private IViewComponentResult Post(ModelBindResult<ContactUsViewModel> result, PageViewModel model)
        //{
        //    result.Model.Page = model;

        //    if (result.ModelState.IsValid)
        //    {
        //        _service.Save(result.Model);
        //        return View("Post", result.Model);
        //    }
        //    else
        //    {
        //        return Get(result.Model);
        //    }
        //}

        public  void haberKaydet()
        {
            //...           
        }
    }
}
