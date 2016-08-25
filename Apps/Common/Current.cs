using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;

namespace Robonom.Web.Core.Apps.Common
{
    /// <summary>
    /// Extension methods, for accessing information about the current request.
    /// </summary>
    public static class Current
    {
        public static IConfiguration Configuration = null;
        // private static IHttpContextAccessor _contextAccessor = HttpContext..RequestServices
        static Current() { }
        internal static void PushConfig(IConfigurationRoot config)
        {
            Configuration = config;
        }

        public static DateTime Now
        {
            get { return DateTime.UtcNow; }
        }
        public static HttpClient HttpClient
        {
            get { return HttpConnectionClient.Current.Client; }
        }

        #region AppSettings

        //public static class Settings
        //{
        public static string AppName
        {
            get { return Configuration.GetSection("App").GetSection("Name").Value; }
        }

        public static string DefaultAPIAddress
        {
            get
            {
                return Configuration.GetSection("App").GetSection("DefaultAPIAddress").Value;
            }
        }

        public static string GetAppConfig(string config)
        {
            return Configuration.GetSection("App").GetSection(config).Value;
        }

        public static string AssetsUrl
        {
            get { return Configuration.GetSection("App").GetSection("AssetsUrl").Value; }
        }

        public static string DefaultLanguage
        {
            get { return Configuration.GetSection("App").GetSection("DefaultLanguage").Value; }
        }

        public static string DefaultPagingSize
        {
            get { return Configuration.GetSection("App").GetSection("DefaultPagingSize").Value; }
        }

        public static string DefaultBrowserTitle
        {
            get { return Configuration.GetSection("App").GetSection("DefaultBrowserTitle").Value; }
        }
        //}

        #endregion

        #region HttpContext

        //public static class HttpCtx
        //{
        //public static HttpContext HttpContext
        //{
        //    get { return _contextAccessor.HttpContext; }
        //}

        public static string RootPath(HttpContext context)
        {

            IHostingEnvironment _hostingEnvironment =
                (IHostingEnvironment)context.RequestServices.GetService(typeof(IHostingEnvironment));
            return _hostingEnvironment.WebRootPath;
        }


        public static string RealPath(HttpContext context)
        {
            IHostingEnvironment _hostingEnvironment =
                               (IHostingEnvironment)context.RequestServices.GetService(typeof(IHostingEnvironment));
            return _hostingEnvironment.WebRootPath + context.Request.Path.ToString().Replace("~/", "/");
        }

        public static bool IsAjaxRequest(HttpContext context)
        {
            HttpRequest request = context.Request;
            if (request == null)
                throw new ArgumentNullException("request");
            if (request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return true;
            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
        //}

        #endregion

        #region Robonom

        //public static class Robonom
        //{
        public static dynamic GetPageInfo(HttpContext context)
        {
            string _requestedPath = context.Request.Path;
            string _urlMethodKey = Current.GetAppConfig("url.method.key");
            string _webRootPath = Current.RootPath(context);
            string _dataRootPath = $"{_webRootPath}{"/data"}";
            string _fileExtension = Current.GetAppConfig("page.file.extension");
            string _pageFilePath = _requestedPath;
            string _pageMethodName = "";
            string _pageMethodParam = "";

            if (string.IsNullOrEmpty(_requestedPath) || _requestedPath.Equals("/")) _pageFilePath = "/default";


            if (_requestedPath.Contains(_urlMethodKey))
            {
                string[] vars = _requestedPath.Split(new string[] { _urlMethodKey }, StringSplitOptions.None);
                _pageFilePath = vars[0].Substring(0, vars[0].Length - 1);

                if (vars[1].Contains("/"))
                {
                    string[] methodParams = vars[1].Split('/');
                    _pageMethodName = methodParams[0];
                    _pageMethodParam = methodParams[1];
                }
                else
                    _pageMethodName = vars[1];

            }

            _pageFilePath = $"{_dataRootPath}{"/rb-pages"}{_pageFilePath}{_fileExtension}";

            dynamic result = null;

            if (File.Exists(_pageFilePath))
            {
                result = JsonConvert.DeserializeObject(File.ReadAllText(_pageFilePath));
                result.FilePath = _pageFilePath;
                result.RequestedPath = _requestedPath;
                result.PageMethod = _pageMethodName;
                result.PageMethodParam = _pageMethodParam;
                //result.HttpContext = context;
            }

            return result;
        }

        public static string GetTemplatePath(dynamic pageInfo, HttpContext context)
        {
            string templatePath = pageInfo.template;
            return Current.RootPath(context) + templatePath.Replace("~/wwwroot", "/");
        }

        public static string DataPath
        {
            get { return Configuration.GetSection("App").GetSection("data.path").Value; }
        }


        public static string AppsPath
        {
            get { return Configuration.GetSection("App").GetSection("apps.path").Value; }
        }
        //}

        #endregion

    }
}
