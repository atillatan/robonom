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

namespace Robonom.Common
{
    /// <summary>
    /// Extension methods, for accessing information about the current request.
    /// </summary>
    public static class Current //TODO: Atilla, i chande name Site like jekyll, i create 3 singleton class like Site (for application scope),
    // User (for session scope), Page (for current request), 
    {
        public static IConfiguration Configuration = null;
        public static IHostingEnvironment Environment = null;
        public static List<dynamic> Pages = new List<dynamic>();

        // private static IHttpContextAccessor _contextAccessor = HttpContext..RequestServices
        static Current() { }
        public static void PushConfig(IConfiguration config, IHostingEnvironment environment)
        {
            Configuration = config;
            Environment = environment;

            string[] files = Directory.GetFiles(environment.ContentRootPath + Current.PagesPath, "*.*", SearchOption.AllDirectories);

            foreach (string path in files)
            {
                if (File.Exists(path))
                {
                    string na = Path.GetFileNameWithoutExtension(path);
                    if (na.StartsWith(".")) continue;
                    dynamic page = null;
                    page = JsonConvert.DeserializeObject(Current.GetFrontMeter(path));
                    page.FilePath = path;
                    Pages.Add(page);
                }
            }
        }

        public static DateTime Now
        {
            get { return DateTime.UtcNow; }
        }
        public static HttpClient HttpClient
        {
            get { return HttpConnectionClient.Current.Client; }
        }



        #region HttpContext

        public static string WebRootPath
        {
            get { return Environment.WebRootPath; }
        }

        public static string ContentRootPath
        {
            get { return Environment.ContentRootPath; }
        }

        public static string RealPath(HttpContext context)
        {
            return RealPath(context.Request.Path);
        }

        public static string RealPath(string relativePath)
        {
            return Environment.ContentRootPath + relativePath.Replace("~/", "/");
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
        public static dynamic GetPageInfo(HttpContext context)
        {
            return GetPageInfo(context.Request.Path);
        }
        public static dynamic GetPageInfo(string requestedPath)
        {
            string _requestedPath = requestedPath;
            string _urlMethodKey = Current.GetConfig("url_method_key");
            string _webRootPath = Current.WebRootPath;
            string _dataRootPath = $"{_webRootPath}{"/App_Data"}";
            string _fileExtension = ".md";//Current.GetAppConfig("page_file_extension");
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

            _pageFilePath = $"{_dataRootPath}{"/pages"}{_pageFilePath}{_fileExtension}";

            dynamic page = null;

            if (File.Exists(_pageFilePath))
            {
                //result = JsonConvert.DeserializeObject(File.ReadAllText(_pageFilePath));                
                page = JsonConvert.DeserializeObject(Current.GetFrontMeter(_pageFilePath));
                page.FilePath = _pageFilePath;
                page.RequestedPath = _requestedPath;
                page.PageMethod = _pageMethodName;
                page.PageMethodParam = _pageMethodParam;
                //result.HttpContext = context;
            }

            return page;
        }

        public static string GetFrontMeter(string pageFilePath)
        {
            string fileContent = File.ReadAllText(pageFilePath);
            int startIndex = fileContent.IndexOf("<!--") + 4;
            int length = fileContent.IndexOf("-->") - startIndex;

            return fileContent.Substring(startIndex, length);
        }

        public static string RemoveFrontMeter(string fileContent)
        {
            return fileContent.Substring(fileContent.IndexOf("-->") + 3);
        }

        public static string GetConfig(string configName)
        {
            return Configuration.GetSection("app").GetSection(configName).Value;
        }
        public static string AppsPath
        {
            get { return GetConfig("apps_path"); }
        }
        public static string AssetPath
        {
            get { return GetConfig("asset_path"); }
        }

        public static string AssetUrl
        {
            get { return GetConfig("asset_path").Replace("/wwwroot", ""); }
        }
        public static string DataPath
        {
            get { return GetConfig("data_path"); }
        }
        public static string LayoutsPath
        {
            get { return GetConfig("layouts_path"); }
        }

        public static string PagesPath
        {
            get { return GetConfig("pages_path"); }
        }

        #endregion



    }
}
