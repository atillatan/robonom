using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Robonom.Web.Core.Apps.Common;

namespace Robonom.Web.Core.wwwroot.apps.rb_editor
{
    [ViewComponent]
    public class RbEditor : ViewComponent
    {
        private IViewComponentHelper _viewComponentHelper;

        public RbEditor(IViewComponentHelper viewComponentHelper)
        {
            _viewComponentHelper = viewComponentHelper;
            Console.WriteLine(_viewComponentHelper.ToString());
        }
 
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string viewPath = $"{"~"}{Current.AppsPath}{"/rb-editor/Index.cshtml"}";
            string filePath = Request.Path;
            if (filePath.Equals("/")) filePath = "/default";

            HtmlString responseHtml = null;
            RbEditorModel Model = new RbEditorModel();
            bool isEditorOpen = true;
            Model.IsEditorOpen = isEditorOpen;
            HttpContext _httpContext = this.HttpContext;



            if (_httpContext.Request.Method == "POST")
            {
                string header = Model.Header;
                //string content = Model.Content;
                 header = _httpContext.Request.Form["header"];
                string content = _httpContext.Request.Form["content"];
                responseHtml = new HtmlString(content);
                Model.ResponseHtml = responseHtml.ToString();
            }

            if (Request.Method == "GET")
            {
                string fileContent = System.IO.File.ReadAllText(Current.RootPath(_httpContext) + Current.DataPath.Replace("/wwwroot","") + "/rb-editor" + filePath + ".html");
                Model.ResponseHtml = new HtmlString(fileContent).ToString();
            }
            await Task.Delay(100);
            return View(viewPath, Model);
        }
    }

    public class RbEditorModel
    {
        public string Header { get; set; }

        public string Content { get; set; }

        public bool IsEditorOpen { get; set; }

        public string ResponseHtml { get; set; }
    }
}
