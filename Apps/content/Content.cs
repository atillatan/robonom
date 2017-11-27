﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Markdig;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Robonom.Common;

namespace  Robonom.Apps
{
    [ViewComponent]
    public class Content : ViewComponent
    {
        private IViewComponentHelper _viewComponentHelper;

        public Content(IViewComponentHelper viewComponentHelper)
        {
            _viewComponentHelper = viewComponentHelper;
            Console.WriteLine(_viewComponentHelper.ToString());
        }
 
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string viewPath = $"{"~"}{Current.AppsPath}{"/content/Index.cshtml"}";
            string filePath = Request.Path;
            if (filePath.Equals("/")) filePath = "/default";

            HtmlString responseHtml = null;
            ContentModel Model = new ContentModel();
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
                string fileContent = System.IO.File.ReadAllText(Current.ContentRootPath + Current.PagesPath + filePath + ".md");
                fileContent=Current.RemoveFrontMeter(fileContent);
                 
                Model.ResponseHtml = new HtmlString(Markdown.ToHtml(fileContent)).ToString();
            }
            await Task.Delay(10);
            return View(viewPath, Model);
        }
    }

    public class ContentModel
    {
        public string Header { get; set; }

        public string Content { get; set; }

        public bool IsEditorOpen { get; set; }

        public string ResponseHtml { get; set; }
    }
}